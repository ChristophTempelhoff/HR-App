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
using System.Windows.Shapes;
using System.IO;

namespace HR_App
{
    /// <summary>
    /// Interaktionslogik für AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();
        }

        void AddUserFunc(object sender, RoutedEventArgs e)
        {
            try
            {
                Backend.Backend backend = new Backend.Backend(File.ReadAllText("Env.txt"));
                Backend.User user = new Backend.User();
                user.firstName = firstName.Text;
                user.lastName = lastName.Text;
                user.age = Int32.Parse(age.Text);
                user.employeeType = type.Text.ToString();
                user.employeeSince = employeeSince.Text;
                user.salary = double.Parse(salary.Text);
                user.insurance = insurance.Text;
                user.userName = username.Text;
                user.password = backend.CreateMD5("1234TEST");

                
                backend.insertPerson(user);
                MessageBox.Show("Success!");
                EmployeeList employeeList = new EmployeeList();
                employeeList.mnuRefresh(null, null);
                this.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
                throw;
            }
        }
    }
}
