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

    }
}
