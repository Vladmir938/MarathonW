using System;
using System.Collections.Generic;
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
using Microsoft.Win32;  // Add this namespace for OpenFileDialog
using System.IO;

namespace MarathonW.Admin
{
    /// <summary>
    /// Логика взаимодействия для VolontMenu.xaml
    /// </summary>
    public partial class VolontMenu : Page
    {
        private DataBase database = new DataBase();
        private string filePath;
        public VolontMenu()
        {
            InitializeComponent();
            VolunteerDG.ItemsSource = database.GetVolunteers();
            txbCountUser.Text = $"{VolunteerDG.Items.Count}";
        }

        private void InsertVolunteer(string firstName, string lastName, string countryCode, int gender)
        {
            string query = "INSERT INTO Volunteer (Surname, Name, Country, Gender) VALUES (@FirstName, @LastName, @CountryCode, @Gender)";

            using (SqlCommand sqlCommand = new SqlCommand(query, database.getConnection()))
            {
                sqlCommand.Parameters.AddWithValue("@FirstName", firstName);
                sqlCommand.Parameters.AddWithValue("@LastName", lastName);
                sqlCommand.Parameters.AddWithValue("@CountryCode", countryCode);
                sqlCommand.Parameters.AddWithValue("@Gender", gender);

                sqlCommand.ExecuteNonQuery();
            }
        }

        private void btnAddVol_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                database.OpenConnection();
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "CSV Files (*.csv)|*.csv|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == true)
                {
                    filePath = openFileDialog.FileName; // Store the selected file path

                    database.OpenConnection();

                    string[] lines = File.ReadAllLines(filePath);

                    // Skip the header row (assuming the header is present)
                    for (int i = 1; i < lines.Length; i++)
                    {
                        string[] columns = lines[i].Split(',');

                        // Assuming the columns in the CSV file are in the correct order
                        string firstName = columns[1];
                        string lastName = columns[2];
                        string countryCode = columns[3];
                        int gender = columns[4].ToUpper() == "F" ? 0 : 1;

                        // Assuming you have a method to insert a volunteer into the database
                        InsertVolunteer(firstName, lastName, countryCode, gender);
                    }

                    MessageBox.Show("Волонтеры загружены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
            finally
            {
                database.CloseConnection();
            }

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Assuming GetVolunteers method fetches the latest data from the database
                VolunteerDG.ItemsSource = database.GetVolunteers();
                txbCountUser.Text = $"{VolunteerDG.Items.Count}";
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }

        }
    }
}
