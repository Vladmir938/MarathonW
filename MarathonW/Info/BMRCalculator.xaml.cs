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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MarathonW.Info
{
    /// <summary>
    /// Логика взаимодействия для BMRCalculator.xaml
    /// </summary>
    public partial class BMRCalculator : Page
    {
        double weight = 0;
        double height = 0;
        double age = 0;
        double BMR = 0;
        public BMRCalculator()
        {
            InitializeComponent();
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Info/InfoMenu.xaml", UriKind.Relative));
        }

        private void btn_calc_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbWeight.Text) || string.IsNullOrWhiteSpace(txbHeight.Text) || string.IsNullOrWhiteSpace(txbAge.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля веса, роста и возраста.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            double weight, height, age, BMR;

            if (!double.TryParse(txbWeight.Text, out weight) || !double.TryParse(txbHeight.Text, out height) || !double.TryParse(txbAge.Text, out age))
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения для веса, роста и возраста.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (check_female.IsChecked == false && check_male.IsChecked == false)
            {
                MessageBox.Show("Вы не выбрали пол.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (check_male.IsChecked == true)
                {
                    BMR = Math.Round(66 + (13.7 * weight) + (5 * height) - (6.8 * age), 1);
                }
                else
                {
                    BMR = Math.Round(65.5 + (9.6 * weight) + (1.8 * height) - (4.7 * age), 1);
                }

                txtYourBMR.Text = Convert.ToString(BMR);
                txtSeat.Text = Convert.ToString(BMR * 1.2);
                txtLowActivity.Text = Convert.ToString(BMR * 1.375);
                txtMidActivity.Text = Convert.ToString(BMR * 1.55);
                txtHighActivity.Text = Convert.ToString(BMR * 1.725);
                txtMaxActivity.Text = Convert.ToString(BMR * 1.9);
            }
        }


        private void btnMale_Click(object sender, RoutedEventArgs e)
        {
            btnFemale.IsEnabled = true;
            check_female.IsChecked = false;
            check_male.IsChecked = true;
            btnMale.IsEnabled = false;
        }

        private void btnFemale_Click(object sender, RoutedEventArgs e)
        {
            btnMale.IsEnabled = true;
            check_male.IsChecked = false;
            check_female.IsChecked = true;
            btnFemale.IsEnabled = false;
        }
    }
}
