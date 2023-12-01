using MarathonW.Runners;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
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
    public partial class Auth : Page
    {
        DataBase dataBase = new DataBase(); // Предполагается, что у вас есть класс DataBase с методами для работы с базой данных.

        public Auth()
        {
            InitializeComponent();
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("StartWindow.xaml", UriKind.Relative));
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = txb_email.Text;
                string password = txb_pass.Password;

                // Проверка наличия введенного email и пароля в базе данных
                if (IsCredentialsValid(email, password))
                {
                    MessageBox.Show("Вы успешно вошли в систему!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    // Здесь вы можете перенаправить пользователя на другую страницу или выполнить другие действия после входа.
                }
                else
                {
                    MessageBox.Show("Неверный email или пароль.", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsCredentialsValid(string email, string password)
        {
            try
            {
                dataBase.OpenConnection();

                string query = "SELECT COUNT(*) FROM Runners WHERE Login = @Email AND Password = @Password";
                using (SqlCommand command = new SqlCommand(query, dataBase.getConnection()))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
            finally
            {
                dataBase.CloseConnection();
            }
        }

        private void txb_email_GotFocus(object sender, RoutedEventArgs e)
        {
            txb_email.Text = "";
        }
    }
}
