using login_img.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace login_img.Controllers
{
   
    public class ProductController : Controller
    {
        FoodDBContext db;
        public ProductController(FoodDBContext db)
        {
            this.db = db;
        }
            public IActionResult Index()
        {
            List<Product> products = db.products.Include(c => c.category).ToList();

            return View(products);
        }

        /* Add products (Admin)*/
        #region Add products
        [HttpGet]
        public IActionResult Add_Product()
        {
            List<category> categories = db.categories.ToList();
            SelectList category_list = new SelectList(categories, "category_ID", "category_name");
            ViewBag.categories = category_list;
            List<Product> products = db.products.ToList();
            ViewBag.products = products;
            return View();
        }
        [HttpPost]
        public IActionResult Add_Product(Product products,IFormFile product_image)
        {
            string Path = $"wwwroot/images/{product_image.FileName}";
            FileStream file = new FileStream(Path, FileMode.Create);
            product_image.CopyTo(file);
            file.Close();
            products.product_image = $"/images/{product_image.FileName}";
            db.products.Add(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        /* Edit products (Admin)*/
        #region Edit products
        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            if (id == null)
            {
                return Content("ID not found");
            }
            Product product = db.products.SingleOrDefault(p => p.product_ID == id);
            List<category> categories = db.categories.ToList();
            SelectList category_list = new SelectList(categories, "category_ID", "category_name");
            ViewBag.categories = category_list;
            if (product == null)
            {
                return RedirectToAction("Index");
            }

            return View(product);
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product product,IFormFile product_image)
        {
            Product newProduct = db.products.SingleOrDefault(n => n.product_ID == product.product_ID);
            newProduct.product_price = product.product_price;
            newProduct.product_name = product.product_name;
            newProduct.category_ID = product.category_ID;
            newProduct.description =product.description;
            string Path = $"wwwroot/images/{product_image.FileName}";
            FileStream file = new FileStream(Path, FileMode.Create);
            product_image.CopyTo(file);
            file.Close();
            newProduct.product_image = $"/images/{product_image.FileName}";
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        /* Delete products (Admin)*/
        #region Delete products
        public IActionResult Remove(int id)
        {
            Product product = db.products.SingleOrDefault(p => p.product_ID == id);
            if (product == null)
            {
                return Content("Not Found");
            }
            db.products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

    }

}
