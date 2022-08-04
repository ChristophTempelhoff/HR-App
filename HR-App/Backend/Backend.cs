using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;

namespace HR_App.Backend
{
    internal class Backend
    {
        string connectionData;
        User user;
        public Backend(string connectionData)
        {
            this.connectionData = connectionData;
        }

        /// <summary>
        /// This function is used to return all or one of the allowed users for this application. It is used for i.e. Login
        /// </summary>
        /// <param name="query">The SQL-Query you'd like executed</param>
        /// <returns>A list of all fitting Users</returns>
        public async Task<List<User>> getUserFromDBAsync(string query)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionData))
                {
                    //MySqlCommand command = new MySqlCommand(query);
                    //command.Connection = connection;
                    MySqlDataReader dataReader;
                    connection.Open();
                    dataReader = await MySqlHelper.ExecuteReaderAsync(connection, query);

                    //store of the last user
                    int id;
                    string firstName;
                    string lastName;
                    int age;
                    string type;
                    DateTime employeeSince;
                    double salary;
                    string insurance;
                    string username;
                    string password;

                    List<User> users = new List<User>();

                    while (dataReader.Read())
                    {
                        id = dataReader.GetInt32(0);
                        firstName = dataReader.GetString(1);
                        lastName = dataReader.GetString(2);
                        age = dataReader.GetInt32(3);
                        type = dataReader.GetString(4);
                        employeeSince = dataReader.GetDateTime(5);
                        salary = dataReader.GetDouble(6);
                        insurance = dataReader.GetString(7);
                        username = dataReader.GetString(8);
                        password = dataReader.GetString(9);

                        if(username != "" || password != "")
                        {
                            users.Add(new User { id = id, firstName = firstName, lastName = lastName, age = age, employeeType = type, employeeSince = employeeSince, salary = salary, insurance = insurance, userName = username, password = password });
                            username = "";
                            password = "";
                        }
                    }
                    connection.Close();
                    return users;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Returns a MD5 hash as a String.
        /// </summary>
        /// <param name="input">The string you would like to receive back as a MD5 hash</param>
        /// <returns>A MD5 hash</returns>
        /// found on https://stackoverflow.com/questions/11454004/calculate-a-md5-hash-from-a-string
        public string CreateMD5(string input) 
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString().ToLower();
            }
        }
    }
}
