using login_img.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Savory_Website.Repository;

namespace login_img.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly IRepository<Admin> repository;

        public AdminController(IRepository<Admin> repository)
        {
            this.repository = repository;
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
            var adminFound = repository.GetAll(a => a.Email == admin.Email && a.Password == admin.Password).SingleOrDefault();
            if(adminFound == null)
            {
                return RedirectToAction("Login");
            }
            HttpContext.Session.SetInt32("Id", adminFound.Id);
            return RedirectToAction("Dashboard");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("login","Customer");
        }
    }
}
