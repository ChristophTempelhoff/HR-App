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
using System.Windows.Navigation;
using System.Windows.Shapes;
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
                List<Backend.User> users = await backend.getUserFromDBAsync(query);

                foreach(var user in users)
                {
                    if(backend.CreateMD5(Password.Password) == user.password && Username.Text == user.userName)
                    {
                        MessageBox.Show("Login successful");
                    }
                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
