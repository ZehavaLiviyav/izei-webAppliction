using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OurNewProject.Data;
using OurNewProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace OurNewProject.Controllers
{
    public class UsersController : Controller
    {
        private readonly OurNewProjectContext _context;

        public UsersController(OurNewProjectContext context)
        {
            _context = context;
        }

      

        // ------------------------------------------------------------------------------

        // GET: Users/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Users/Register
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,UserName,Password")] User user)
        {
            if (ModelState.IsValid)
            {

                var q = _context.User.FirstOrDefault(u => u.UserName == user.UserName);

                if (q == null)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();

                    var u = _context.User.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
                    Signin(u);

                    return RedirectToAction(nameof(Index), "Home");

                }
                else
                {
                    ViewData["Error"] = "Username is already taken";
                }

            }
            return View(user);
        }


        //-------------------------------------------------------------------------------

        public async Task<IActionResult> Logout()
        {
            //HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("LogIn");
        }


        // ------------------------------------------------------------------------------

        // GET: Users/LogIn
        public IActionResult LogIn()
        {
            return View();
        }

        // POST: Users/LogIn
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Id,UserName,Password")] User user)
        {
            if (ModelState.IsValid)
            {

                var q = _context.User.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);

                if (q != null)
                {

                    Signin(q);
                    return RedirectToAction(nameof(Index), "Home");
                }
                else
                {
                    ViewData["Error"] = "Username or password are incorrect";
                }

            }
            return View(user);
        }




        private async void Signin(User account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.UserName),
                new Claim(ClaimTypes.Role, account.Type.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10)
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }




        // ===============================================================================================


        // GET: Users/Delete/5





        // GET: Users
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: Users/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,UserName,Password,Type")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password,Type")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SearchU(string query)
        {
            try
            {
                return Json(await _context.User.Where(c => c.UserName.Contains(query)).ToListAsync());

            }
            catch { return RedirectToAction("PageNotFound", "Home"); }
        }



        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Statistics()
        {
            //statistic 1- how many orders every customer had made, there is only one shopping cart
            ICollection<Stat> statistic1 = new Collection<Stat>();
            var result1 = from c in _context.User.Include(o => o.Type).GroupBy(u => u.Type)
                          select c.Count();
            int countUser = 0, countAdmin = 0;
            foreach (var u in (from c in _context.User select c))
            {
                if (u.Type == UserType.Client)
                    countUser++;
                else
                    countAdmin++;
            }
            statistic1.Add(new Stat("Client", countUser));
            statistic1.Add(new Stat("Admin", countAdmin));

            ViewBag.data = statistic1;
            ICollection<Stat> statistic2 = new Collection<Stat>();
            List<Product> products = _context.Product.ToList();
            List<Category> categories = _context.Category.ToList();
            var res2 = from prod in products
                       join cat in categories on prod.CategoryId equals cat.Id
                       group cat by cat.Id into Gr
                       select new { id = Gr.Key, num = Gr.Count() };

            var stat = from g in res2
                       join cat in categories on g.id equals cat.Id
                       select new { category = cat.Name, count = g.num };
            foreach (var v in stat)
            {
                if (v.count > 0)
                    statistic2.Add(new Stat(v.category, v.count));
            }

            ViewBag.data2 = statistic2;

            return View();
        }

        internal class ObjectResult
        {
            public string Type { get; set; }
            public int count { get; set; }
            public ObjectResult(string t, int c) { Type = t; count = c; }
        }
        public IActionResult AccessDenied()
        {
            return View();
        }


    }
    public class Stat
    {
        public string Key;
        public int Values;
        public Stat(string key, int values)
        {
            Key = key;
            Values = values;
        }
    }


}