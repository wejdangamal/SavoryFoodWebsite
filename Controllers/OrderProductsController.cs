using login_img.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Savory_Website.Repository;

namespace login_img.Controllers
{
    public class OrderProductsController : Controller
    {
        private readonly IRepository<OrderProduct> repository;

        public OrderProductsController(IRepository<OrderProduct> repository)
        {
            this.repository = repository;
        }

        // GET: OrderProducts
        public IActionResult Index()
        {
            int Cid = (int)HttpContext.Session.GetInt32("Id");
            var foodDBContext = repository.GetAll(c => c.customer_id == (int)HttpContext.Session.GetInt32("Id"), new string[] { "customer", "products" }).ToList();
            if (foodDBContext == null)
            {
                return RedirectToAction("Home", "Customer");
            }
            return View(foodDBContext);
        }
        // GET: OrderProducts/Delete/5
        public IActionResult Delete(int? id)
        {
            int Cid = (int)HttpContext.Session.GetInt32("Id");
            if (id == null)
            {
                return NotFound();
            }

            var orderProduct = repository.GetAll(m => m.productId == id&&m.customer_id==Cid, new string[] { "customer", "products" })
                .FirstOrDefault();
            if (orderProduct == null)
            {
                return NotFound();
            }

            return View(orderProduct);
        }
        // POST: OrderProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int Cid = (int)HttpContext.Session.GetInt32("Id");
            if (repository == null)
            {
                return Problem("Entity set 'FoodDBContext.orderProducts'  is null.");
            }
            var orderProduct =  repository.GetAll(c => c.productId == id && c.customer_id == Cid).FirstOrDefault();

            if (orderProduct != null)
            {
               var result = await repository.Delete(orderProduct);
            }
            return RedirectToAction(nameof(Index));
        }
        private bool OrderProductExists(int id)
        {
            return repository.GetAll().Any(e => e.customer_id == id);
        }
    }
}
