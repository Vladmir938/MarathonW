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

namespace MarathonW
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Page
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Auth.xaml", UriKind.Relative));
        }
        private void btn_about_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_sponsor_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_runner_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Runners/RegRunner.xaml", UriKind.Relative));
        }
    }
}
