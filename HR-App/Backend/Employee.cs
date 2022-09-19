using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_App.Backend
{
    //This Class is used to represent the user and any other employee
    public class Employee
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        public string employeeType { get; set; }
        public string employeeSince { get; set; }
        public double salary { get; set; }
        public string insurance { get; set; }
    }
}
