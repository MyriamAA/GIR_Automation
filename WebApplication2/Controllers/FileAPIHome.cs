using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    public class FileAPIHome : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Other(){
          return View();
        }
    }
}
