using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using wedding_planner.Models;
using System.Linq;

namespace wedding_planner.Controllers
{
    public class LoginController : Controller
        {
            private WeddingContext _context;
 
            public LoginController(WeddingContext context)
            {
                _context = context;
            }

        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.Errors = new List<string>();
            return View();
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterViewModel validation)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User
                {
                    Fname = validation.fname,
                    Lname = validation.lname,
                    Email = validation.email,
                    Password = validation.password,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                _context.Users.Add(newUser);
                _context.SaveChanges();
            HttpContext.Session.SetInt32("user_id", newUser.UserId);
            return RedirectToAction("dashboard", "wedding");
                
            }
            else{
                ViewBag.Errors = ModelState.Values;
                return View("index");
            }
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(string email, string password)
        {
            List<User> ReturnedEmail = _context.Users.Where(user => user.Email == email).ToList();
            if(ReturnedEmail.Count > 0)
            {
                if(ReturnedEmail[0].Password == password)
                {
                    HttpContext.Session.SetInt32("user_id", ReturnedEmail[0].UserId);
                    return RedirectToAction("dashboard","wedding");
                }
                // else{
                //     ViewBag.Errors = new List<string>();
                //     ViewBag.PasswordErrors = "Password does not match email address. Please try again";
                //     return View("index");

                // }
            }
            // else
            // {
            //         ViewBag.Errors = new List<string>();
            //         ViewBag.EmailErrors = "Email does not exist. Please register a new account";
            //         return View("index");

            // }
            ViewBag.Errors = new List<string>();
            ViewBag.EmailErrors = "Invalid Combination";
            return View("index");
        }


    }
}
