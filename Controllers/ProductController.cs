using login_img.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using Savory_Website.Repository;

namespace login_img.Controllers
{

    public class ProductController : Controller
    {
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<category> categoryRepository;

        public ProductController(IRepository<Product> productRepository, IRepository<category> categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            List<Product> products = productRepository.GetAll(new string[] { "category" }).ToList();
            return View(products);
        }

        /* Add products (Admin)*/
        #region Add products
        [HttpGet]
        public IActionResult Add_Product()
        {
            List<category> categories = categoryRepository.GetAll().ToList();
            SelectList category_list = new SelectList(categories, "category_ID", "category_name");
            ViewBag.categories = category_list;
            List<Product> products = productRepository.GetAll().ToList();
            ViewBag.products = products;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add_Product(Product products, IFormFile product_image)
        {
            string Path = $"wwwroot/images/{product_image.FileName}";
            FileStream file = new FileStream(Path, FileMode.Create);
            product_image.CopyTo(file);
            file.Close();
            products.product_image = $"/images/{product_image.FileName}";
            await productRepository.Add(products);
            return RedirectToAction("Index");
        }
        #endregion
        /* Edit products (Admin)*/
        #region Edit products
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            Product product = await productRepository.GetById(id);
            List<category> categories = categoryRepository.GetAll().ToList();
            SelectList category_list = new SelectList(categories, "category_ID", "category_name");
            ViewBag.categories = category_list;
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Product product, IFormFile product_image)
        {
            Product newProduct =await productRepository.GetById(product.product_ID);
            newProduct.product_price = product.product_price;
            newProduct.product_name = product.product_name;
            newProduct.category_ID = product.category_ID;
            newProduct.description = product.description;
            string Path = $"wwwroot/images/{product_image.FileName}";
            FileStream file = new FileStream(Path, FileMode.Create);
            product_image.CopyTo(file);
            file.Close();
            newProduct.product_image = $"/images/{product_image.FileName}";
            await productRepository.Update(newProduct);
            return RedirectToAction("Index");
        }
        #endregion
        /* Delete products (Admin)*/
        #region Delete products
        public async Task<IActionResult> Remove(int id)
        {
            var result = await productRepository.DeleteById(id);
            if (result)
                return RedirectToAction("Index");
            return Content("Not Found");
        }
        #endregion

    }

}
