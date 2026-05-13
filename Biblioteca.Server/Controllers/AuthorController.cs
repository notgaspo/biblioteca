using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : Controller
    {   
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
