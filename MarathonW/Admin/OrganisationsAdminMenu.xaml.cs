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

namespace MarathonW.Admin
{
    /// <summary>
    /// Логика взаимодействия для OrganisationsAdminMenu.xaml
    /// </summary>
    public partial class OrganisationsAdminMenu : Page
    {
        private DataBase database = new DataBase();

        public OrganisationsAdminMenu()
        {
            InitializeComponent();
            FundDb.ItemsSource = database.GetOrganizations();
        }

        private void ImageFailedHandler(object sender, ExceptionRoutedEventArgs e)
        {
            // Handle image loading failure
            MessageBox.Show($"Image loading failed: {e.ErrorException.Message}");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


        }

        private void btnAddFund_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
