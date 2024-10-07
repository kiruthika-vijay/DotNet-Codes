using Microsoft.Extensions.Configuration;
using Segue201DemoApp.entity;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Segue201DemoApp.DataConnectContext
{
    public class ProductDataContext : DataContext
    {
        public Table<Product> Products;
        public ProductDataContext(string connectionString) : base(connectionString) { }
    }
}

