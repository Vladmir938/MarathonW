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
    /// Логика взаимодействия для EditCharity.xaml
    /// </summary>
    public partial class EditCharity : Page
    {
        private DataBase database = new DataBase();
        private int organizationId; // Переменная для хранения идентификатора организации
        public EditCharity()
        {
            InitializeComponent();
            this.organizationId = organizationId;

            // Загрузка данных об организации при открытии формы

        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Admin/AdminMenu.xaml", UriKind.Relative));
        }

        private void btn_Reg_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
