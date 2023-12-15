using login_img.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Savory_Website.Data;
using Savory_Website.Models.ViewModels;
using Savory_Website.Repository;

namespace login_img.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IRepository<Customer> _CustomerRepository;
        private readonly IRepository<Admin> adminRepository;
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<OrderProduct> _orderProductRepository;

        public CustomerController(IRepository<Customer> CustomerRepository, IRepository<Admin> adminRepository, IRepository<Product> productRepository
            , IRepository<OrderProduct> orderProductRepository)
        {
            _CustomerRepository = CustomerRepository;
            this.adminRepository = adminRepository;
            this.productRepository = productRepository;
            _orderProductRepository = orderProductRepository;
        }
        [HttpGet]
        public IActionResult Home()
        {
            int? userId = (int)HttpContext.Session.GetInt32("Id");
            if (userId == null)
            {
                return RedirectToAction("login");
            }
            List<Product> products = productRepository.GetAll().ToList();
            return View(products);
        }
        public IActionResult Menu(int? id)
        {
            int? userId = (int)HttpContext.Session.GetInt32("Id");
            if (userId == null)
            {
                return RedirectToAction("login");
            }
            List<Product> products = productRepository.GetAll(p => p.category_ID == id).ToList(); 
            if (products.Count == 0 || products == null)
            {
                products = productRepository.GetAll().ToList();
            }
            return View(products);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _CustomerRepository.Add(customer);
                HttpContext.Session.SetInt32("Id", customer.Id);
                return RedirectToAction("Home");
            }
            return Content(ModelState.ErrorCount.ToString());
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
            Customer customer = _CustomerRepository.GetAll(c => c.email == log.email && c.password == log.password).SingleOrDefault();
            Admin admin = adminRepository.GetAll(c => c.Email == log.email && c.Password == log.password).SingleOrDefault();
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

        public async Task<IActionResult> AddCart(int id)
        {
            OrderProduct op = new OrderProduct();

            op.productId = id;

            if ((int)HttpContext.Session.GetInt32("Id") != null)
            {
                op.customer_id = (int)HttpContext.Session.GetInt32("Id");
                op.Date = DateTime.Today.ToString("dd-MM-yyyy");
                op.Price = productRepository.GetAll(p => p.product_ID == id).Select(p => p.product_price).SingleOrDefault();
                try
                {
                   await _orderProductRepository.Add(op);
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
