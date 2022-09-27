using System;
using System.Collections.Generic;
using System.Windows;
using HR_App.Backend;
using System.IO;

namespace HR_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "SELECT * FROM employees";
                string connectionData = File.ReadAllText("Env.txt");
                Backend.Backend backend = new Backend.Backend(connectionData);
                List<User> users = await backend.getUserFromDBAsync(query);

                foreach(var user in users)
                {
                    if(backend.CreateMD5(Password.Password) == user.password && Username.Text == user.userName)
                    {
                        EmployeeList employeeList = new EmployeeList();
                        employeeList.Show();
                        this.Close();
                        return;
                    }
                }
                MessageBox.Show("Username or password wrong!");
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
