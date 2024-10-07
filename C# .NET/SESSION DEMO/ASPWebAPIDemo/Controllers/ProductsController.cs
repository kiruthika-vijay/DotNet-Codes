using ASPWebAPIDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product> products = new List<Product>()
        {
            new Product { Id = 1, Name="Laptop", Price=10000.00m, Category="Electronics"},
            new Product { Id = 2, Name="Mobile", Price=1200.00m, Category="Electronics"},
            new Product { Id = 3, Name="Tablet", Price=1400.00m, Category="Electronics"},
            new Product { Id = 4, Name="Power bank", Price=1300.00m, Category="Electronics"},
            new Product { Id = 5, Name="USB device", Price=1100.00m, Category="Electronics"}
        };

        // [Route("Products/All")]
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {            
            return products;
        }

        //[Route("Products/ById/{id}")]
        //[HttpGet]
        //public IActionResult GetProductByID(int id)
        //{
        //    var listProducts = products.Where(p => p.Id == id).ToList();
        //    if (!listProducts.Any()) // Check if the list is empty
        //    {
        //        return NotFound();
        //    }
        //    return Ok(listProducts);
        //}
        
        [Route("{id:int}")]
        [HttpGet]
        public string GetProductByIntID(int id)
        {
            return $"Message returned from GetProduct by Id INT Mapping : {id}";
        }
        
        [Route("{cat:alpha}")]
        [HttpGet]
        public string GetProductByAlphaCat(string cat)
        {
            return $"Message returned from GetProduct by Category ALPHA Mapping : {cat}";
        }
        
        [Route("Products/ByCat/{cat}/ByPri/{pri}")]
        [HttpGet]
        public IActionResult GetProductByCatPrice(string cat, decimal pri)
        {
            var product = products.Where(p => p.Category == cat && p.Price == pri).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        
        [Route("Products/ByCat/{cat}")]
        [HttpGet]
        public IActionResult GetProductByCategory(string cat)
        {
            var product = products.Where(p => p.Category == cat).ToList();
            if (!product.Any())
            {
                return NotFound();
            }
            return Ok(product);
        }
        
        [Route("Products/PriceSorted/{sortOrder}")]
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProductsSorted(string sortOrder)
        {
            var product = products.AsQueryable();

            switch (sortOrder?.ToLower())
            {
                case "desc":
                    product = product.OrderByDescending(p => p.Price);
                    break;
                case "asc":
                default:
                    product = product.OrderBy(p => p.Price);
                    break;
            }

            return Ok(product.ToList());
        }

        [Route("Products/Post")]
        [HttpPost]
        public ActionResult<Product> PostAllProducts(Product pro)
        {
            products.Add(pro);
            return CreatedAtAction(nameof(GetAllProducts), new {id=pro.Id}, pro);
        }
    }
}
