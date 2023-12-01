using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Data.SqlClient;

namespace MarathonW.Runners
{
    public partial class RegRunner : Page
    {
        DataBase dataBase = new DataBase();

        public RegRunner()
        {
            InitializeComponent();
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("StartWindow.xaml", UriKind.Relative));
        }

        private bool IsEmailAlreadyRegistered(string email)
        {
            try
            {
                dataBase.OpenConnection(); // Открываем соединение

                // Проверка наличия email в базе данных
                string query = "SELECT COUNT(*) FROM Runners WHERE Login = @Email";
                using (SqlCommand command = new SqlCommand(query, dataBase.getConnection()))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
            finally
            {
                dataBase.CloseConnection(); // Закрываем соединение в блоке finally, чтобы гарантировать, что оно будет закрыто даже в случае ошибки
            }
        }

        private void btn_Reg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = txb_email.Text;
                string password = txb_pass.Text;
                string repeatPassword = txb_repeatpass.Text;
                string name = txb_name.Text;
                string patronymic = txb_patronymic.Text;
                string surname = txb_surname.Text;
                string gender = (string)((ListBoxItem)cmb_gender.SelectedItem)?.Content;
                string photoPath = txb_pathphoto.Text;
                DateTime? birthDate = dateBirth.SelectedDate;
                string country = (string)((ListBoxItem)cmbCountry.SelectedItem)?.Content;

                // Validate data
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(repeatPassword) ||
                    string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(patronymic) || string.IsNullOrWhiteSpace(surname) ||
                    string.IsNullOrWhiteSpace(gender) || birthDate == null || string.IsNullOrWhiteSpace(country))
                {
                    MessageBox.Show("Нужно заполнить все поля!.", "Ошибка при регистрации", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Validate email format
                if (!IsValidEmail(email))
                {
                    MessageBox.Show("Неверно указана почта.", "Ошибка при регистрации", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Validate password match
                if (password != repeatPassword)
                {
                    MessageBox.Show("Пароли не совпадают.", "Ошибка при регистрации", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (IsEmailAlreadyRegistered(email))
                {
                    MessageBox.Show("Пользователь с таким email уже зарегистрирован.", "Ошибка при регистрации", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // TODO: Add more specific validation if needed
                int genderValue = (gender.ToLower() == "мужской") ? 1 : 0;


                string imagesFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "RunnersPhoto");

                // Получаем только имя файла из пути в TextBox
                string fileName = Path.GetFileName(txb_pathphoto.Text);

                // Создаем относительный путь для сохранения в базе данных
                string relativePath = $"../../../Images/RunnersPhoto/{fileName}";

                // Копируем изображение в папку проекта (если не существует)
                string destinationPath = Path.Combine(imagesFolderPath, fileName);
                File.Copy(txb_pathphoto.Text, relativePath, true);


                dataBase.OpenConnection();

                string sqlInsert = "INSERT INTO Runners (Login, Password, Name, Patronymic, Surname, Gender, Photo, Date_of_birth, Country) " +
                                   "VALUES (@Email, @Password, @Name, @Patronymic, @Surname, @Gender, @PhotoPath, @BirthDate, @Country)";

                using (SqlCommand command = new SqlCommand(sqlInsert, dataBase.getConnection()))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Patronymic", patronymic);
                    command.Parameters.AddWithValue("@Surname", surname);
                    command.Parameters.AddWithValue("@Gender", genderValue);
                    command.Parameters.AddWithValue("@PhotoPath", relativePath); // Используем относительный путь
                    command.Parameters.AddWithValue("@BirthDate", birthDate);
                    command.Parameters.AddWithValue("@Country", country);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Вы успешно зарегистрировались!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.NavigationService.Navigate(new Uri("StartWindow.xaml", UriKind.Relative));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                dataBase.CloseConnection();
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private string GetRelativePath(string basePath, string fullPath)
        {
            Uri baseUri = new Uri(basePath);
            Uri fullUri = new Uri(fullPath);
            return baseUri.MakeRelativeUri(fullUri).ToString();
        }

        private void btn_source_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == true)
                {
                    // Получаем путь к выбранному файлу
                    string imagePath = openFileDialog.FileName;

                    // Отображаем изображение на форме
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(imagePath);
                    bitmap.EndInit();

                    imgAvatar.Source = bitmap;

                    // Сохраняем путь в TextBox
                    txb_pathphoto.Text = imagePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
