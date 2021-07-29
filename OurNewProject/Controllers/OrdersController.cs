using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OurNewProject.Data;
using OurNewProject.Models;

namespace OurNewProject.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OurNewProjectContext _context;

        public OrdersController(OurNewProjectContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            try
            {
                List<Order> applicationDbContext = _context.Order.Include(c => c.UserID).Include(p => p.MyProductList).ToList();

                foreach (Order or in applicationDbContext)
                {
                    or.UserID = _context.User.FirstOrDefault(u => u.Id == or.UserID).Id;
                }
                return View(applicationDbContext);
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }



            /*return View(await _context.Order.ToListAsync());*/
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("PageNotFound", "Home");
            }

            var order = await _context.Order
                  .Include(p => p.MyProductList)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return RedirectToAction("PageNotFound", "Home");
            }
            order.UserID = _context.User.FirstOrDefault(u => u.Id == order.UserID).Id;

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["Productss"] = new SelectList(_context.Product, nameof(Product.Id), nameof(Product.Name));
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,UserID,TimeOrder,Address,TotalPrice")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,UserID,TimeOrder,Address,TotalPrice")] Order order)
        {
            if (id != order.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderID == id);
        }

        public IActionResult MyOrder()
        {
            try
            {
                String userName = HttpContext.User.Identity.Name;
                User user = _context.User.FirstOrDefault(x => x.UserName.Equals(userName));
                Order order = _context.Order.FirstOrDefault(x => x.UserID == user.Id);
                order.MyProductList = _context.Product.Where(x => x.MyOrderList.Contains(order)).ToList();

                if (order == null)
                {
                    return RedirectToAction("PageNotFound", "Home");
                }

                return View(order);
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }


        public async Task<IActionResult> AddToOrder(int id) //product id
        {
            try
            {
                Product product = _context.Product.Include(db => db.MyOrderList).FirstOrDefault(x => x.Id == id);
                String userName = HttpContext.User.Identity.Name;
                User user = _context.User.FirstOrDefault(x => x.UserName.Equals(userName));
                Order order = _context.Order.Include(db => db.MyProductList)
                 .FirstOrDefault(x => x.UserID == user.Id);


                if (order.UserID == null)
                    order.MyProductList = new List<Product>();
                if (product.MyOrderList == null)
                    product.MyOrderList = new List<Order>();

                if (!(order.MyProductList.Contains(product) && product.MyOrderList.Contains(order)))
                {

                    order.MyProductList.Add(product);
                    product.MyOrderList.Add(order);
                    order.TotalPrice += product.Price;
                    _context.Update(order);
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(MyOrder));
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }
    }
}
