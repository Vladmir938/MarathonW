using MarathonW.Admin;
using MarathonW.Info;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace MarathonW
{
    class DataBase
    {
        private SqlConnection sqlConnection = new SqlConnection(@"Data Source=E404-10\SQLEXPRESS;Initial catalog=MarathonW;User ID=sa;Password=sa2021;");

        public ObservableCollection<Organization> GetOrganizations()
        {
            ObservableCollection<Organization> organizations = new ObservableCollection<Organization>();

            try
            {
                OpenConnection();

                string query = "SELECT Logo, Name, Description FROM Organisations";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Organization org = new Organization
                    {
                        Logo = reader["Logo"].ToString(),
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                    };

                    organizations.Add(org);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
            finally
            {
                CloseConnection();
            }

            return organizations;
        }

        public ObservableCollection<Volunteer> GetVolunteers()
        {
            ObservableCollection<Volunteer> volunteers = new ObservableCollection<Volunteer>();

            try
            {
                OpenConnection();

                string query = "SELECT Surname, Name, Country, Gender FROM Volunteer";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    Volunteer volunteer = new Volunteer
                    {
                        Surname = reader["Surname"].ToString(),
                        Name = reader["Name"].ToString(),
                        Country = reader["Country"].ToString(),
                        GenderNumeric = Convert.ToInt32(reader["Gender"]),
                    };

                    volunteers.Add(volunteer);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
            finally
            {
                CloseConnection();
            }

            return volunteers;
        }

        public SqlConnection getConnection()
        {
            return sqlConnection;
        }

        public void OpenConnection()
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
    }
}
