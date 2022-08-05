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
    /// Interaktionslogik für AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        public AddEmployee()
        {
            InitializeComponent();
        }

        void AddEmployeeFunc(object sender, RoutedEventArgs e)
        {
            try
            {
                Backend.Employee employee = new Backend.Employee();
                employee.firstName = firstName.Text;
                employee.lastName = lastName.Text;
                employee.age = Int32.Parse(age.Text);
                employee.employeeType = type.Text.ToString();
                employee.employeeSince = employeeSince.Text;
                employee.salary = double.Parse(salary.Text);
                employee.insurance = insurance.Text;

                Backend.Backend backend = new Backend.Backend(File.ReadAllText("Env.txt"));
                backend.insertPerson(employee);
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
