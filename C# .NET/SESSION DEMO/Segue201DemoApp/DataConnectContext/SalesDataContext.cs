using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Segue201DemoApp.entity;

namespace Segue201DemoApp.DataConnectContext
{
    public class SalesDataContext : DataContext
    {
        public Table<Orders> Orders;
        public Table<Stores> Stores;
        public Table<Customers> Customers;
        public SalesDataContext(string connectionString) : base(connectionString) { }
    }
}
