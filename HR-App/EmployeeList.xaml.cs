using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace HR_App
{
    /// <summary>
    /// Interaktionslogik für EmployeeList.xaml
    /// </summary>
    public partial class EmployeeList : Window
    {
        DataGrid employeeList;
        public EmployeeList()
        {
            InitializeComponent();
            employeeList = employeeListDataGrid;
            fillDataGrid();
        }


        async void fillDataGrid()
        {
            string connectionData = File.ReadAllText("Env.txt");
            Backend.Backend backend = new Backend.Backend(connectionData);
            List<Backend.Employee> employees = await backend.getEmployeeFromDBAsync("SELECT * FROM employees");
            foreach (var employee in employees)
            {
                employeeList.Items.Add(employee);
            }
        }
        private void mnuAddUser(object sender, RoutedEventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.Show();
        }
        private void mnuAddEmployee(object sender, RoutedEventArgs e)
        {
            AddEmployee addEmployee = new AddEmployee();
            addEmployee.Show();
        }
        public void mnuRefresh(object sender, RoutedEventArgs e)
        {
            employeeList.Items.Clear();
            fillDataGrid();
        }

        private async void employeeListDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Backend.Employee employee = (Backend.Employee)employeeList.SelectedItem;
            if(employee != null)
            {
                Backend.Backend backend = new Backend.Backend(File.ReadAllText("Env.txt"));
                List<Backend.User> users = await backend.getUserFromDBAsync("SELECT * FROM employees WHERE id = " + employee.id);
                Backend.User user = users[0];
                EditPerson editPerson = new EditPerson(user, this);
                editPerson.Show();
            }
        }

        private void employeeListDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2) employeeListDataGrid_MouseDoubleClick(sender, null); return;
        }
    }
}
