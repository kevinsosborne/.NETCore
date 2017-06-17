using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Wall.Models;
using Newtonsoft.Json;


namespace Wall.Controllers
{
    public class LoginController : Controller

    {
        private readonly DbConnector _dbConnector;
 
        public LoginController(DbConnector connect)
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

        [HttpPost]
        [Route("/register")]

        public IActionResult register(User NewUser)
       {
            if (ModelState.IsValid){
                 _dbConnector.Execute($"INSERT INTO Users(firstname, lastname, email, password, created_at, updated_at) VALUES ('{NewUser.fname}', '{NewUser.lname}', '{NewUser.email}', '{NewUser.password}', now(), now());");
                List<Dictionary<string, object>> QueryId = _dbConnector.Query($"Select * from users where email = '{NewUser.email}'");
                HttpContext.Session.SetString("user_name", NewUser.fname);
                HttpContext.Session.SetInt32("user_id", (int)QueryId[0]["id"]);
                 return RedirectToAction("Wall", "Wall"); 
             }
            else{
 		        System.Console.WriteLine("This is ModelState.Values");
                 System.Console.WriteLine(ModelState.Values);
                 ViewBag.errors = ModelState.Values;
                return View("Index");
            }
            
        }

        [HttpPost]
        [Route("login")]
        public IActionResult login(string email, string password)
        {   
             List<Dictionary<string,object>> QueryEmail = _dbConnector.Query($"SELECT * FROM Users where email = '{email}'");
            if(QueryEmail.Count > 0)
            {
                System.Console.WriteLine("In Query Email != Null. We found a User based on email");
                if((string)QueryEmail[0]["password"] == password)
                {
                    HttpContext.Session.SetString("user_name", (string)QueryEmail[0]["firstname"]);
                    HttpContext.Session.SetInt32("user_id", (int)QueryEmail[0]["id"]);
                    return RedirectToAction("Wall", "Wall");   
                }
                else{
                    ViewBag.passworderror = "User password does not match email";
                    ViewBag.errors = new List<string>();
                    return View("index");
                }
            } 
            else{
 		        ViewBag.emailerror = "User email is invalid";
                ViewBag.errors = new List<string>();
                return View("Index");
            }
           
        }
    }
}
