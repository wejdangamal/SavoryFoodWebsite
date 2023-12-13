using login_img.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace login_img.Controllers
{
    public class OrderProductsController : Controller
    {
        private readonly FoodDBContext _context;

        public OrderProductsController(FoodDBContext context)
        {
            _context = context;
        }

        // GET: OrderProducts
        public async Task<IActionResult> Index()
        {
            int Cid = (int)HttpContext.Session.GetInt32("Id");
            var foodDBContext = _context.orderProducts.Include(o => o.customer).Include(o => o.products).Where(c => c.customer_id == (int)HttpContext.Session.GetInt32("Id"));
            if (foodDBContext == null)
            {
                return RedirectToAction("Home", "Customer");
            }
            return View(await foodDBContext.ToListAsync());
        }



        // GET: OrderProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            int Cid = (int)HttpContext.Session.GetInt32("Id");
            if (id == null || _context.orderProducts == null)
            {
                return NotFound();
            }

            var orderProduct = await _context.orderProducts
                .Include(o => o.customer)
                .Include(o => o.products)
                .FirstOrDefaultAsync(m => m.productId == id);
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
            if (_context.orderProducts == null)
            {
                return Problem("Entity set 'FoodDBContext.orderProducts'  is null.");
            }
            //  var orderProduct = await _context.orderProducts.FindAsync(id);
            var orderProduct = await _context.orderProducts.SingleOrDefaultAsync(c => c.productId == id && c.customer_id == Cid);
            if (orderProduct != null)
            {
                _context.orderProducts.Remove(orderProduct);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderProductExists(int id)
        {
            return _context.orderProducts.Any(e => e.customer_id == id);
        }
    }
}
