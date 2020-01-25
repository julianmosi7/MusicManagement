using DBLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            db = new MusicContext("OrderContext").SeedIfEmpty();
            viewModel = new ViewModel(db);
            
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

        //drag&drop

        private void InitDragDrop(ViewModel viewModel)
        {
            dataGrid.AllowDrop = true;
            dataGrid.DragOver += DataGrip_DragOver;
            dataGrid.Drop += DataGrip_Drop;
            dataGrid.DragEnter += (s, e) => dataGrid.Background = Brushes.SkyBlue;
            dataGrid.DragLeave += (s, e) => dataGrid.Background = Brushes.White;
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
            dataGrid.Background = Brushes.White;
        }
    }
}
