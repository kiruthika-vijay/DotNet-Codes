using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Segue201DemoApp.entity
{
    [System.Data.Linq.Mapping.Table(Name = "Sales.Customers")]
    public class Customers
    {
        #region LINQ Data Mapping
        [System.Data.Linq.Mapping.Column(IsPrimaryKey = true)]
        public int customer_id { get; set; }
        [System.Data.Linq.Mapping.Column]
        public string first_name { get; set; }
        [System.Data.Linq.Mapping.Column]
        public string last_name { get; set; }
        [System.Data.Linq.Mapping.Column]
        public string phone { get; set; }
        [System.Data.Linq.Mapping.Column]
        public string email { get; set; }
        [System.Data.Linq.Mapping.Column]
        public string street { get; set; }
        [System.Data.Linq.Mapping.Column]
        public string city { get; set; }
        [System.Data.Linq.Mapping.Column]
        public string state { get; set; }
        [System.Data.Linq.Mapping.Column]
        public string zip_code { get; set; }
        #endregion

        //public int CustomerID { get; set; }
        //public string CustomerName { get; set; }
        //public List<Orders> orders { get; set; }
    }
}
