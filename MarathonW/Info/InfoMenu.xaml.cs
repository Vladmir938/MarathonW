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
    /// Логика взаимодействия для InfoMenu.xaml
    /// </summary>
    public partial class InfoMenu : Page
    {
        public InfoMenu()
        {
            InitializeComponent();
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("StartWindow.xaml", UriKind.Relative));
        }

        private void btn_bmrcalc_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Info/BMRCalculator.xaml", UriKind.Relative));
        }

        private void btn_bmicalc_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Info/BMICalculator.xaml", UriKind.Relative));
        }
    }
}
