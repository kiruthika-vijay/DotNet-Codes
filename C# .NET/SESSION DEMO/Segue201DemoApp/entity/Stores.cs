using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Segue201DemoApp.entity
{
    //[System.Data.Linq.Mapping.Table(Name = "Sales.Stores")]
    public class Stores
    {
        #region LINQ Data Mapping
        //[System.Data.Linq.Mapping.Column(IsPrimaryKey = true)]
        //public int store_id { get; set; }
        //[System.Data.Linq.Mapping.Column]
        //public string store_name { get; set; }
        //[System.Data.Linq.Mapping.Column]
        //public string phone { get; set; }
        //[System.Data.Linq.Mapping.Column]
        //public string email { get; set; }
        //[System.Data.Linq.Mapping.Column]
        //public string street { get; set; }
        //[System.Data.Linq.Mapping.Column]
        //public string city { get; set; }
        //[System.Data.Linq.Mapping.Column]
        //public string state { get; set; }
        //[System.Data.Linq.Mapping.Column]
        #endregion
        //public string zip_code { get; set; }

        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public List<Product> Products { get; set; }
    }
}
