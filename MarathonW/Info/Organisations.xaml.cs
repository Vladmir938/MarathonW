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

namespace MarathonW.Info
{
    /// <summary>
    /// Логика взаимодействия для Organisations.xaml
    /// </summary>
    public partial class Organisations : Page
    {
        private DataBase database = new DataBase();

        public Organisations()
        {
            InitializeComponent();
            FundDb.ItemsSource = database.GetOrganizations();
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Info/InfoMenu.xaml", UriKind.Relative));
        }

        private void ImageFailedHandler(object sender, ExceptionRoutedEventArgs e)
        {
            // Handle image loading failure
            MessageBox.Show($"Image loading failed: {e.ErrorException.Message}");
        }
    }
}
