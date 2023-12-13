using login_img.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace login_img.Controllers
{
    
    public class AdminController : Controller
    {
        FoodDBContext db;
        public AdminController(FoodDBContext db)
        {
            this.db = db;
        }
        
        public IActionResult Dashboard()
        {
            int? AdminId = (int) HttpContext.Session.GetInt32("Id");
            if (AdminId == null)
            {
                return RedirectToAction("login");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Admin admin)
        {
            Admin admins = db.admins.SingleOrDefault(a => a.Email == admin.Email && a.Password == admin.Password);
            if(admins == null)
            {
                return RedirectToAction("Login");
            }
            HttpContext.Session.SetInt32("Id", admins.Id);
            return RedirectToAction("Dashboard");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("login","Customer");
        }
    }
}
