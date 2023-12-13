using login_img.Models;
using Microsoft.AspNetCore.Mvc;

namespace login_img.Controllers
{
    public class categoryController : Controller
    {
        FoodDBContext db;
        public categoryController(FoodDBContext db)
        {
            this.db = db;
        }
        public IActionResult Categories()
        {
            List<category> Categories = db.categories.ToList();
            return View(Categories);
        }

        /* Add products (Admin)*/
        #region Add Category
        [HttpGet]
        public IActionResult Add()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Add(category category)
        {
            category new_category = new category();
            new_category.category_name = category.category_name;
            try
            {
                db.categories.Add(new_category);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Content("sorry!");
            }
            return RedirectToAction("Categories");
        }
        #endregion
        /* Edit products (Admin)*/
        #region Edit Category
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Add");
            }
            category category = db.categories.SingleOrDefault(c => c.category_ID == id);
            if(category == null)
            {
                return Content("Not Found");
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(category category)
        {
            category cat = db.categories.SingleOrDefault(c => c.category_ID == category.category_ID);
            if (cat == null)
            {
                return Content("Not Found");
            }
            cat.category_name = category.category_name;
            db.SaveChanges();
            return RedirectToAction("Categories");
        }
        #endregion
        /* Delete products (Admin)*/
        #region Delete Category
        public IActionResult delete(int id)
        {
            category category = db.categories.SingleOrDefault(c => c.category_ID == id);
            if(category == null)
            {
                return Content("Already deleted");
            }
            db.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Categories");
        }
        #endregion
    }
}
