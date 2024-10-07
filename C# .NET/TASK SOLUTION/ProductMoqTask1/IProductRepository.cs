using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMoqTask1
{
    public interface IProductRepository
    {
        decimal GetPrice(int productId);
    }
}
