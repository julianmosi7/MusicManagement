using DBLib;
using DBLibrary;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConnectionString();
            var db = new MusicContext("OrderContext").SeedIfEmpty();
            var viewModel = new ViewModel(db);
            DataContext = viewModel;
            AccessDatabase(db);
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
    }
}
