using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Segue201DemoApp.method
{
    public class OrderProcessor
    {
        public void PlaceOrder(double orderAmount)
        {
            if (orderAmount < 0)
            {
                throw new exception.InvalidOrderException("Order amount cannot be negative.");
            }
            Console.WriteLine($"Order placed successfully with amount : {orderAmount}");
        }
    }
}
