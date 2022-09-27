using System;
using System.Windows;
using System.IO;
using System.Windows.Documents;
using System.Collections.Generic;

namespace HR_App
{
    /// <summary>
    /// Interaktionslogik für EditPerson.xaml
    /// </summary>
    public partial class EditPerson : Window
    {
        Backend.User user;
        Backend.Employee employee;
        EmployeeList employeeList;
        public EditPerson(Backend.User user, EmployeeList employeeList)
        {
            InitializeComponent();
            this.employee = user;
            this.user = user;
            this.employeeList = employeeList;
            firstName.Text = user.firstName;
            lastName.Text = user.lastName;
            age.Text = user.age.ToString();
            type.Text = user.employeeType;
            since.Text = user.employeeSince;
            salary.Text = user.salary.ToString();
            insurance.Text = user.insurance;
            username.Text = user.userName;
            password.Password = user.password;
        }

        private void SaveUser(object sender, RoutedEventArgs e)
        {
            Backend.Backend backend = new Backend.Backend(File.ReadAllText("Env.txt"));
            user.firstName = firstName.Text;
            user.lastName = lastName.Text;
            user.age = Int32.Parse(age.Text);
            user.employeeType = type.Text;
            user.employeeSince = since.Text;
            user.salary = double.Parse(salary.Text);
            user.insurance = insurance.Text;
            user.userName = username.Text;
            user.password = backend.CreateMD5(password.Password);
            backend.updatePerson(user);
            MessageBox.Show("User edited successfully!");
            employeeList.mnuRefresh(this, null);
            this.Close();
        }

        private async void DeleteUser(object sender, RoutedEventArgs e)
        {
            Backend.Backend backend = new Backend.Backend(File.ReadAllText("Env.txt"));
            backend.deletePerson(user);
            MessageBox.Show("User deleted successfully!");
            employeeList.mnuRefresh(this, null);
            this.Close();
        }
    }
}
