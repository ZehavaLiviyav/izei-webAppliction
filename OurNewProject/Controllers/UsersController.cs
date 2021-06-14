using System;
using System.Collections.Generic;
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

                    return RedirectToAction(nameof(Index),"Home");

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
        public async Task<IActionResult> LogIn([Bind("Id,UserName,Password")] User user)
        {
            if (ModelState.IsValid){

                    var q = _context.User.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);

                if (q != null){

                    Signin(q);
                    return RedirectToAction(nameof(Index), "Home");
                }
                else{
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

        /*
                public void DeleteUser(User user)
                {
                    var a = _context.User.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);

                    User use = _context.User.Find(a);


                }*/
        public IActionResult Delete() { return View(); }
        public IActionResult Edit() { return View(); }

    }
}
