using login_img.Models;
using Microsoft.AspNetCore.Mvc;
using Savory_Website.Repository;

namespace login_img.Controllers
{
    public class categoryController : Controller
    {
        private readonly IRepository<category> _repository;
        public categoryController(IRepository<category> repository)
        {
            _repository = repository;
        }
        public IActionResult Categories()
        {
            List<category> Categories = _repository.GetAll().ToList();
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
        public async Task<IActionResult> Add(category category)
        {
            category new_category = new category();
            new_category.category_name = category.category_name;
            try
            {
                await _repository.Add(new_category);
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
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Add");
            }
            category category = await _repository.GetById(id);
            if (category == null)
            {
                return Content("Not Found");
            }
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(category category)
        {
            category cat = await _repository.GetById(category.category_ID);
            if (cat == null)
            {
                return Content("Not Found");
            }
            cat.category_name = category.category_name;
            await _repository.Update(cat);
            return RedirectToAction("Categories");
        }
        #endregion
        /* Delete products (Admin)*/
        #region Delete Category
        public async Task<IActionResult> delete(int id)
        {
            var result = await _repository.DeleteById(id);
            if (result)
                return RedirectToAction("Categories");
            return Content("Not Found");
        }
        #endregion
    }
}
