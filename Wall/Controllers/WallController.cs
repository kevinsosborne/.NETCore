using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Wall.Models;

namespace Wall.Controllers
{
    public class WallController : Controller
    
    {
        private readonly DbConnector _dbConnector;
 
        public WallController(DbConnector connect)
        {
            _dbConnector = connect;
        }
 
        [HttpGet]
        [Route("wall")]
        public IActionResult Wall()
        {
            // ViewBag.errors = new List<string>();
            string user_name = HttpContext.Session.GetString("user_name");
            ViewBag.user_name = user_name;
            List<Dictionary<string,object>> AllMessages = _dbConnector.Query("Select messages.id As message_id, message, Messages.created_at, Messages.updated_at, firstname from Messages JOIN Users ON Users.id = messages.users_id ORDER BY  Messages.created_at DESC;");
            System.Console.WriteLine("All Messages", AllMessages);
            ViewBag.AllMessages = AllMessages;
            List<Dictionary<string, object>> QueryComments = _dbConnector.Query("Select comments.id AS comment_id, comments.messages_id, users.firstname, comments.created_at, comments.comment from comments JOIN messages ON comments.messages_id = messages.id JOIN users ON comments.users_id = users.id Order By comments.created_at DESC;");
            ViewBag.Comments = QueryComments;
            return View();
        }

        [HttpPost]
        [Route("message")]
        public IActionResult message(string message)
        {   
            int? user_id = HttpContext.Session.GetInt32("user_id");
            _dbConnector.Execute($"INSERT INTO Messages(message, updated_at, created_at, users_id) VALUES ('{message}', NOW(), NOW(), {user_id})");
            // List<Dictionary<string, object>> QueryMessage_id = _dbConnector.Query($"Select * From Messages where message = '{message}'");
            // HttpContext.Session.SetInt32("message_id", (int)QueryMessage_id[0]["id"]);
            return RedirectToAction("wall");
        }
        [HttpPost]
        [Route("comment")]
        public IActionResult comment(string comment, string message_id)
        {   
            int? user_id = HttpContext.Session.GetInt32("user_id");
            // int? message_id = HttpContext.Session.GetInt32("message_id");
            _dbConnector.Execute($"INSERT INTO Comments(comment, updated_at, created_at, Messages_id, users_id) VALUES ('{comment}', NOW(), NOW(),{message_id}, {user_id})");
            return RedirectToAction("wall");
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index", "Login");
        }



    }
}