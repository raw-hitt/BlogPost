using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Login()
        {
            return View();
        }
        
        public IActionResult AddBlog()
        {
            return View();
        }
        public IActionResult EditBlog()
        {
            return View();
        }
    }
}
