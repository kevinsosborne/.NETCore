using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using RESTauranter.Models;


namespace RESTauranter.Controllers
{
    public class HomeController : Controller
    {
        private RESTauranterContext _context;
    
        public HomeController(RESTauranterContext context)
        {
            _context = context;
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
        [Route("/process")]
        public IActionResult Process(Review NewReview)
        {
        if(ModelState.IsValid)
        {
            NewReview.created_at = DateTime.Now;
            _context.Add(NewReview);
            _context.SaveChanges();
            return RedirectToAction("Reviews");
        }
        else{
            ViewBag.errors = ModelState.Values;
            return View("Index");
        }
        }
        [HttpGet]
        [Route("reviews")]
        public IActionResult Reviews()
        {
            ViewBag.AllReviews = _context.Review.OrderByDescending(u => u.created_at);
            return View("Reviews");
        }
    }
}
