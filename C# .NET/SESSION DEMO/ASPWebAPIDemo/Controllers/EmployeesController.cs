using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebAPIDemo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return " Message from Get method from Employee Controller";
        }
        
        [HttpGet]
        public string GetEmployee()
        {
            return " Message from Get Employee method from Employee Controller";
        }
    }
}
