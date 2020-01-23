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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConnectionString();
            var db = new MusicContext("OrderContext");
            var viewModel = new ViewModel(db);
            DataContext = viewModel;
            AccessDatabase(db);
        }

        private void ConnectionString()
        {
            
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
