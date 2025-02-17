﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OurNewProject.Data;
using OurNewProject.Models;

namespace OurNewProject.Controllers
{
    public class ProductsController : Controller
    {
        private readonly OurNewProjectContext _context;

        public ProductsController(OurNewProjectContext context)
        {
            _context = context;
        }

        // GET: Products
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var imagesa = _context.Product.Include(p => p.productImage);
            var ourNewProjectContext = _context.Product.Include(p => p.Category);
            return View(await ourNewProjectContext.ToListAsync());
        }





        // GET: Products/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["Products"] = new List<Product>(_context.Product);
            ViewData["Categoriess"] = new SelectList(_context.Category, nameof(Category.Id), nameof(Category.Name));

            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,CategoryId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
   
       
        public async Task<IActionResult> Menu()
        {
            ViewData["Categoriess"] = new SelectList(_context.Category, nameof(Category.Id), nameof(Category.Name));
            var ProjectContext = _context.Product.Include(c => c.Category);
            var query =
                from product in _context.Product
                join image in _context.ProductImage on product.Id equals image.productId
                select new ProductJoin(product, image);
            return View("Menu", await query.ToListAsync());
        }

        public async Task<IActionResult> Buttom(string ctN)
        {


           var outNewP = _context.Product.Include(c => c.Category).Where(p => p.Category.Name.Equals(ctN) ||
                                    (ctN == null));

            var query = from p in outNewP join image in _context.ProductImage on p.Id equals image.productId select new ProductJoin(p, image);

            return View("Menu", await query.ToListAsync());

        }

        public async Task<IActionResult> Search(string productName, string price)
        {
            ViewData["Categoriess"] = new SelectList(_context.Category, nameof(Category.Id), nameof(Category.Name));
            try
            {
                int priceInt = Int32.Parse(price);
                //Get all products
                var products = from p in _context.Product select p;
                //Filter by name
                products = products.Where(x => x.Name.Contains(productName));
                products = products.Where(x => x.Price <= priceInt);
               
                var query = from p in products join image in _context.ProductImage on p.Id equals image.productId select new ProductJoin(p, image);
                
                return View("MenuSearch", await query.ToListAsync());
            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }
    }


    public class ProductJoin
    {
        public Product p { get; set; }
        public ProductImage pi { get; set; }

        public ProductJoin(Product p, ProductImage pi) { this.p = p; this.pi = pi; }
    }
}
