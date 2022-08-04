using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_App.Backend
{
    internal class User : Employee
    {
        public string? userName { get; set; }
        public string? password { get; set; }
    }
}
