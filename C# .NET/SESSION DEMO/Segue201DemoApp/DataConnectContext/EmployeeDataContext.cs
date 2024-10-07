using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Segue201DemoApp.entity;

namespace Segue201DemoApp.DataConnectContext
{
    public class EmployeeDataContext : DataContext
    {
        public Table<Employee> Employees;
        public EmployeeDataContext(string connectionString) : base(connectionString) { }
    }
}
