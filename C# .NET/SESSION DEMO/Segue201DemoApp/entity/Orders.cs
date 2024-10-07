using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Segue201DemoApp.entity
{
    //[System.Data.Linq.Mapping.Table(Name = "Sales.Orders")]
    public class Orders
    {
        #region LINQ Data Mapping
        //[System.Data.Linq.Mapping.Column(IsPrimaryKey = true)]
        //public int order_id { get; set; }
        //[System.Data.Linq.Mapping.Column]
        //public int customer_id { get; set; }
        //[System.Data.Linq.Mapping.Column]
        //public byte order_status { get; set; }
        //[System.Data.Linq.Mapping.Column]
        //public int store_id { get; set; }
        //[System.Data.Linq.Mapping.Column]
        //public int staff_id { get; set; }
        #endregion
        public int OrderID { get; set; }
        public double OrderAmount { get; set; }
    }
}
