using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace MarathonW
{
    public partial class Auth : Page
    {
        DataBase dataBase = new DataBase();

        private int loginAttempts = 0;
        private DateTime blockEndTime;
        private bool isBlocked = false;
        private int maxBlockTime = 180; // Максимальное время блокировки в секундах
        private int blockTimeIncrease = 15; // Увеличение времени блокировки в секундах
        private int attemptsPerIncrease = 3; // Количество попыток перед увеличением времени блокировки

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

                if (IsBlocked())
                {
                    MessageBox.Show($"Слишком много неудачных попыток. Попробуйте снова через {GetRemainingBlockTime()} секунд.", "Блокировка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (IsCredentialsValid(email, password))
                {
                    MessageBox.Show("Вы успешно вошли в систему!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetAttempts();
                    // Здесь вы можете перенаправить пользователя на другую страницу или выполнить другие действия после входа.
                }
                else
                {
                    MessageBox.Show("Неверный email или пароль.", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Warning);
                    IncrementAttempts();
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

        private void IncrementAttempts()
        {
            loginAttempts++;

            if (loginAttempts % attemptsPerIncrease == 0)
            {
                int currentBlockTime = loginAttempts / attemptsPerIncrease * blockTimeIncrease;

                // Проверка на достижение максимального лимита блокировки
                if (currentBlockTime > maxBlockTime)
                {
                    currentBlockTime = maxBlockTime;
                }

                blockEndTime = DateTime.Now.AddSeconds(currentBlockTime);
                isBlocked = true;
            }
        }

        private bool IsBlocked()
        {
            return isBlocked && DateTime.Now < blockEndTime;
        }

        private int GetRemainingBlockTime()
        {
            return (int)Math.Ceiling((blockEndTime - DateTime.Now).TotalSeconds);
        }

        private void ResetAttempts()
        {
            loginAttempts = 0;
            blockEndTime = DateTime.MinValue;
            isBlocked = false;
        }
    }
}
