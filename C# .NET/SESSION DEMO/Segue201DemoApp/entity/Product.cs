using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Segue201DemoApp.entity
{
    //[System.Data.Linq.Mapping.Table(Name = "Products")]
    public class Product
    {
        #region LINQ Data Mapping
        //[System.Data.Linq.Mapping.Column(IsPrimaryKey = true)]
        //public int ProductId { get; set; }
        //[System.Data.Linq.Mapping.Column]
        //public string ProductName { get; set; }
        //[System.Data.Linq.Mapping.Column]
        public decimal Price { get; set; }
        //[System.Data.Linq.Mapping.Column]
        #endregion
        public string Category { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
