using login_img.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Savory_Website.Models.ViewModels;

namespace login_img.Controllers
{
    public class CustomerController : Controller
    {
        FoodDBContext db;
        public CustomerController(FoodDBContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public IActionResult Home()
        {
            int? userId = (int)HttpContext.Session.GetInt32("Id");
            if (userId == null)
            {
                return RedirectToAction("login");
            }
            //Customer customer = db.customers.SingleOrDefault(c => c.Id == userId);
            //  HttpContext.Session.SetInt32("Id", customer.Id);
            List<Product> products = db.products.ToList();
            //  OrderProductVM op = new OrderProductVM();
            // ViewBag.OrderProductVM = op;
            return View(products);
        }
        public IActionResult Menu(int? id)
        {

            int? userId = (int)HttpContext.Session.GetInt32("Id");
            if (userId == null)
            {
                return RedirectToAction("login");
            }
            List<Product> products = db.products.Where(p => p.category_ID == id).ToList();
            if (products.Count == 0 || products == null)
            {
                products = db.products.ToList();
            }
            return View(products);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Customer customer)
        {
            if (customer == null)
            {
                return NotFound();
            }
            db.customers.Add(customer);
            db.SaveChanges();
            // home 
            HttpContext.Session.SetInt32("Id", customer.Id);
            return RedirectToAction("Home");
        }
        [HttpGet]
        public IActionResult login()
        {
            LoginVM logv = new LoginVM();
            return View(logv);
        }
        [HttpPost]
        public IActionResult login(LoginVM log)
        {
            Customer customer = db.customers.SingleOrDefault(c => c.email == log.email && c.password == log.password);
            Admin admin = db.admins.SingleOrDefault(c => c.Email == log.email && c.Password == log.password);
            LoginVM logv = new LoginVM();
            if (customer == null && admin == null)
            {

                logv.Message = "wrong";
                return RedirectToAction("login", logv);
            }
            //save Id of current user
            else if (customer != null && admin == null)
            {
                HttpContext.Session.SetInt32("Id", customer.Id);
                return RedirectToAction("Home");
            }


            HttpContext.Session.SetInt32("Id", admin.Id);
            return RedirectToAction("Dashboard", "Admin");


        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("login");
        }

        public IActionResult AddCart(int id)
        {

            OrderProduct op = new OrderProduct();

            op.productId = id;

            if ((int)HttpContext.Session.GetInt32("Id") != null)
            {

                op.customer_id = (int)HttpContext.Session.GetInt32("Id");
                op.Date = DateTime.Today.ToString("dd-MM-yyyy");
                op.Price = db.products.Where(p => p.product_ID == id).Select(p => p.product_price).SingleOrDefault();
                try
                {
                    db.orderProducts.Add(op);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                  
                    return RedirectToAction("Index", "OrderProducts");
                }
            }

            return RedirectToAction("Index", "OrderProducts");
        }
        
    }
}
