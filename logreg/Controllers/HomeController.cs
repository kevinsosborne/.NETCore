using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using logreg.Models;
using Newtonsoft.Json;


namespace logreg.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbConnector _dbConnector;
 
        public HomeController(DbConnector connect)
        {
            _dbConnector = connect;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()

        {
            ViewBag.errors = new List<string>();
            return View();
        }
        // [HttpGet]
        // [Route("/success")]
        // public IActionResult Success()

        // {
        //     ViewBag.errors = new List<string>();
        //     return View();
        // }

        [HttpPost]
        [Route("/register")]
        public IActionResult Register(string firstname, string lastname, int age, string email, string password)
        {
            User NewUser = new User
            {
                FirstName = firstname,
                LastName = lastname,
                Age = age,
                Email = email,
                Password = password
            };
            TryValidateModel(NewUser);
            if(TryValidateModel(NewUser) == false){
                ViewBag.errors = ModelState.Values;
                // return RedirectToAction("index");
                return View("Index");
            }
            else{
                _dbConnector.Execute($"Insert into user (fname, lname, age, email, password, created_at) values ('{firstname}', '{lastname}', {age}, '{email}', '{password}', now());");
                return View("success");
            }
        }
        [HttpPost]
        [Route("/login")]
        public IActionResult Login(string email, string password)
        {
        _dbConnector.Query($"Select * from user where email = email")
           return View("success");
        }
    }
}
