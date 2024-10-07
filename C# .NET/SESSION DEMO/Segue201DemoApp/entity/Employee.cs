using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Segue201DemoApp.entity
{
    [System.Data.Linq.Mapping.Table(Name = "Employee")]
    public class Employee
    {
        [System.Data.Linq.Mapping.Column(IsPrimaryKey = true)]
        public int EmpId { get; set; }
        [System.Data.Linq.Mapping.Column]
        public string Name { get; set; }
        [System.Data.Linq.Mapping.Column]
        public decimal Salary { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
