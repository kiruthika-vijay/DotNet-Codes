using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProductMoqTask1
{
    public class ProductService
    {
        private readonly IProductRepository productRepository;
        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public decimal CalculatedDiscountPrice(int productId, decimal discountPercentage)
        {
            var price = productRepository.GetPrice(productId);
            if(price == 0)
            {
                throw new ArgumentException("Product not found");
            }
            var discount = (price * discountPercentage) / 100;
            return price - discount;
        }
    }
}
