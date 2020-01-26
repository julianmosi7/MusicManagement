using DBLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ViewModelLib;

namespace MusicManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private ViewModel viewModel;
        private MusicContext db;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConnectionString();
            db = new MusicContext("MusicContext").SeedIfEmpty();
            db.Configuration.ProxyCreationEnabled = false;
            viewModel = new ViewModel(db);
            this.DataContext = viewModel;
            LoadTree();
            
            DataContext = viewModel;
            AccessDatabase(db);
            InitDragDrop(viewModel);

        }

        private void ConnectionString()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine($"path    ={path}");
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }

        private void AccessDatabase(MusicContext db)
        {
            try
            {
                int nr = db.Artists.Count();
                Console.WriteLine($"Nr Employees = {nr}");
                Title = $"Nr Employees = {nr}";
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }       

        private void LoadTree()
        {
            List<Artist> artistsList;            
            TreeViewItem root = new TreeViewItem();

            artistsList = db.Artists.ToList();
            root.Header = "Interpreten";
            treeView.Items.Add(root);

                             
           
            foreach (var item in artistsList.Select(x => x.ArtistName.Substring(0, 1)).Distinct().ToList())
            {
                TreeViewItem child = new TreeViewItem();                
                child.Header = $"{item} [{artistsList.Where(x => x.ArtistName.Substring(0,1).Equals(item)).Count()}]";
                root.Items.Add(child);
                foreach (var item2 in artistsList.Where(x => x.ArtistName.Substring(0,1) == item).ToList())
                {
                    TreeViewItem grandchild = new TreeViewItem();
                    grandchild.Header = item2;
                    child.Items.Add(grandchild);
                    if (item2.Records == null) return;
                    foreach (var item3 in item2.Records)
                    {
                        TreeViewItem doublegrandchild = new TreeViewItem();
                        doublegrandchild.Header = item3.RecordTitle;
                        grandchild.Items.Add(doublegrandchild);
                    }
                }
            }
        }

        private void TreeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var ele = e.OriginalSource as DependencyObject;
            TreeViewItem treeViewItem = (TreeViewItem)GetTreeViewItem(ele as DependencyObject);
            Console.WriteLine(treeViewItem.Header.ToString());
            if (treeViewItem == null) return;            
            string item = treeViewItem.Header.ToString();
           
            
            Record record = db.Records.Where(x => x.RecordTitle == item).FirstOrDefault();
            Console.WriteLine(record.RecordTitle);
            string recTitle = record.RecordTitle;
            
           
            viewModel.SelectedRecord = record;
            
        }

        private void TreeView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var ele = e.OriginalSource as DependencyObject;
            TreeViewItem treeViewItem = (TreeViewItem)GetTreeViewItem(ele as DependencyObject);
            if (treeViewItem == null) return;
            string item = treeViewItem.Header.ToString();
            //var artist = db.Artists.Where(x => x.ArtistName.Equals(item)).FirstOrDefault();
            //Console.WriteLine(artist.ArtistName);
            DragDrop.DoDragDrop(treeViewItem, item, DragDropEffects.All);
            
        }

        private object GetTreeViewItem(DependencyObject ele)
        {
            while(ele != null && !(ele is TreeViewItem))
            {
                ele = VisualTreeHelper.GetParent(ele);
            }
            return ele as TreeViewItem;
        }

        //drag&drop

        private void InitDragDrop(ViewModel viewModel)
        {
            dataGridRecords.AllowDrop = true;
            dataGridRecords.DragOver += DataGrip_DragOver;
            dataGridRecords.Drop += DataGrip_Drop;
            dataGridRecords.DragEnter += (s, e) => dataGridRecords.Background = Brushes.SkyBlue;
            dataGridRecords.DragLeave += (s, e) => dataGridRecords.Background = Brushes.White;
        }

        private void ListBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var listBoxItem = sender as ListBoxItem;
            var artist = listBoxItem.Content as Artist;
            if (artist == null) return;
            DragDrop.DoDragDrop(listBoxItem, artist, DragDropEffects.All);
        }

        private void DataGrip_DragOver(object sender, DragEventArgs e)
        {
            bool isStringData = e.Data.GetDataPresent(typeof(Artist));
            e.Effects = isStringData
                ? DragDropEffects.Copy
                : DragDropEffects.None;
            e.Handled = true;
        }

        private void DataGrip_Drop(object sender, DragEventArgs e)
        {
            Artist artist = e.Data.GetData(typeof(Artist)) as Artist;
            viewModel.SelectedArtist = artist;
            dataGridRecords.Background = Brushes.White;
        }       
    }
}
