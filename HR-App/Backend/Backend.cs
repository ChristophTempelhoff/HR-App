using System;
using System.Collections.Generic;
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
                    DateTime employeeSinceDT;
                    string employeeSince;
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
                        employeeSinceDT = dataReader.GetDateTime(5);
                        employeeSince = employeeSinceDT.Date.ToString();
                        salary = dataReader.GetDouble(6);
                        insurance = dataReader.GetString(7);
                        if(!dataReader.IsDBNull(8) && !dataReader.IsDBNull(9))
                        {
                            username = dataReader.GetString(8);
                            password = dataReader.GetString(9);
                        }
                        else
                        {
                            username = null;
                            password = null;
                        }

                        if(username != null || password != null)
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
        /// Returns one or all employees contained in the table specified in the parameter "query". All that is done async.
        /// </summary>
        /// <param name="query">The SQL-query used to perform the request.</param>
        /// <returns>Returns a List of all the empolyees fitting the SQL-querry</returns>
        public async Task<List<Employee>> getEmployeeFromDBAsync(string query) 
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionData))
                {
                    connection.Open();
                    MySqlDataReader dataReader = await MySqlHelper.ExecuteReaderAsync(connection, query);

                    //store of the last user
                    int id;
                    string firstName;
                    string lastName;
                    int age;
                    string type;
                    DateTime employeeSinceDT;
                    string employeeSince;
                    double salary;
                    string insurance;

                    List<Employee> employees = new List<Employee>();

                    while (dataReader.Read())
                    {
                        id = dataReader.GetInt32(0);
                        firstName = dataReader.GetString(1);
                        lastName = dataReader.GetString(2);
                        age = dataReader.GetInt32(3);
                        type = dataReader.GetString(4);
                        employeeSinceDT = dataReader.GetDateTime(5);
                        employeeSince = employeeSinceDT.Day + "." + employeeSinceDT.Month + "." + employeeSinceDT.Year;
                        salary = dataReader.GetDouble(6);
                        insurance = dataReader.GetString(7);

                        employees.Add(new Employee { id = id, firstName = firstName, lastName = lastName, age = age, employeeType = type, employeeSince = employeeSince, salary = salary, insurance = insurance });
                    }

                    connection.Close();
                    return employees;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }

        /// <summary>
        /// This funtion is used to insert data into any database and is mostly sql-injection-proof
        /// </summary>
        /// <param name="employee">The employee you would like to add.</param>
        public void insertPerson(Employee employee)
        {
            try
            {
                string query = "INSERT INTO employees VALUES(@id,@firstName,@lastName,@age,@employeeType,@employeeSince,@salary,@insurance,@username,@password)";
                List<MySqlParameter> parameters = new List<MySqlParameter>();
                parameters.Add(new MySqlParameter("@id", employee.id));
                parameters.Add(new MySqlParameter("@firstName", employee.firstName));
                parameters.Add(new MySqlParameter("@lastName", employee.lastName));
                parameters.Add(new MySqlParameter("@age", employee.age));
                parameters.Add(new MySqlParameter("@employeeType", employee.employeeType));
                parameters.Add(new MySqlParameter("@employeeSince", employee.employeeSince));
                parameters.Add(new MySqlParameter("@salary", employee.salary));
                parameters.Add(new MySqlParameter("@insurance", employee.insurance));
                parameters.Add(new MySqlParameter("@username", null));
                parameters.Add(new MySqlParameter("@password", null));
                using(MySqlConnection connection = new MySqlConnection(connectionData))
                {
                    MySqlCommand command = new MySqlCommand(query);
                    command.Connection = connection;
                    foreach (var param in parameters)
                    {
                        command.Parameters.Add(param);
                    }
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }

        /// <summary>
        /// This funtion is used to insert data into any database and is mostly sql-injection-proof
        /// </summary>
        /// <param name="user">The user you would like to add.</param>
        public void insertPerson(User user)
        {
            try
            {
                string query = "INSERT INTO employees VALUES(@id,@firstName,@lastName,@age,@employeeType,@employeeSince,@salary,@insurance,@username,@password)";
                List<MySqlParameter> parameters = new List<MySqlParameter>();
                parameters.Add(new MySqlParameter("@id", user.id));
                parameters.Add(new MySqlParameter("@firstName", user.firstName));
                parameters.Add(new MySqlParameter("@lastName", user.lastName));
                parameters.Add(new MySqlParameter("@age", user.age));
                parameters.Add(new MySqlParameter("@employeeType", user.employeeType));
                parameters.Add(new MySqlParameter("@employeeSince", user.employeeSince));
                parameters.Add(new MySqlParameter("@salary", user.salary));
                parameters.Add(new MySqlParameter("@insurance", user.insurance));
                parameters.Add(new MySqlParameter("@username", user.userName));
                parameters.Add(new MySqlParameter("@password", user.password));
                using (MySqlConnection connection = new MySqlConnection(connectionData))
                {
                    MySqlCommand command = new MySqlCommand(query);
                    command.Connection = connection;
                    foreach (var param in parameters)
                    {
                        command.Parameters.Add(param);
                    }
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
        }

        /// <summary>
        /// This function is used to update a Person
        /// </summary>
        /// <param name="user">The employee/user you'd like to update.</param>
        public void updatePerson(User user)
        {
            try
            {
                List<MySqlParameter> parameters = new List<MySqlParameter>();
                parameters.Add(new MySqlParameter("@firstName", user.firstName));
                parameters.Add(new MySqlParameter("@lastName", user.lastName));
                parameters.Add(new MySqlParameter("@age", user.age));
                parameters.Add(new MySqlParameter("@employeeType", user.employeeType));
                parameters.Add(new MySqlParameter("@employeeSince", user.employeeSince));
                parameters.Add(new MySqlParameter("@salary", user.salary));
                parameters.Add(new MySqlParameter("@insurance", user.insurance));
                parameters.Add(new MySqlParameter("@username", user.userName));
                parameters.Add(new MySqlParameter("@password", user.password));
                parameters.Add(new MySqlParameter("@id", user.id));
                string query = "UPDATE employees SET firstName = @firstName, lastName = @lastName, age = @age, employeeType = @employeeType, employeeSince = @employeeSince, salary = @salary, insurance = @insurance, username = @username, password = @password WHERE id = @id";
                using (MySqlConnection connection = new MySqlConnection(connectionData))
                {
                    MySqlCommand command = new MySqlCommand(query);
                    command.Connection = connection;
                    foreach (var param in parameters)
                    {
                        command.Parameters.Add(param);
                    }
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
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
