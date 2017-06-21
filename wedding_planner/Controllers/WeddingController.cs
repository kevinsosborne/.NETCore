using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using wedding_planner.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace wedding_planner.Controllers
{
    public class WeddingController : Controller
        {
            private WeddingContext _context;
 
            public WeddingController(WeddingContext context)
            {
                _context = context;
            }
            [HttpGet]
            [Route("dashboard")]
            public IActionResult dashboard()
            {
                // List<Wedding> AllWeddings = _context.Weddings.ToList();
                // ViewBag.AllWeddings = AllWeddings;
                List<Wedding> AllWeddings = _context.Weddings
                        .Include(w => w.Guests)
                        .ToList();
                ViewBag.AllWeddings = AllWeddings;
                ViewBag.UserID = HttpContext.Session.GetInt32("user_id");
                
    //             List<Wedding> PeopleAttanding = _context.Weddings
    // .Include( p => p.Guest )
    // .ToList();
                return View("dashboard");
            }
            [HttpGet]
            [Route("/plan")]
            public IActionResult Plan()
            {
                ViewBag.PlanErrors = new List<string>();
                return View();
            }
            [HttpGet]
            [Route("/logout")]
            public IActionResult Logout()
            {
                HttpContext.Session.Clear();
                return RedirectToAction("index", "Login");
            }
            [HttpPost]
            [Route("/planwed")]
            public IActionResult planwed(WeddingViewModel Wedding)
            {
                if(ModelState.IsValid)
                {
                    Wedding NewWedding = new Wedding
                    {
                        WedderOne = Wedding.wedder_one,
                        WedderTwo = Wedding.wedder_two,
                        Date = Wedding.date,
                        Address = Wedding.address,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        UserId = (int)HttpContext.Session.GetInt32("user_id")
                    };
                    _context.Weddings.Add(NewWedding);
                    _context.SaveChanges();
                    return RedirectToAction("dashboard");
                }
                else
                {
                    ViewBag.PlanErrors = ModelState.Values;
                    return View("plan");


                }
                
            }
            [HttpGet]
            [Route("/display/{id}")]
            public IActionResult Display(int id)
            {
                Wedding CurrWedding = _context.Weddings.Where(wedding => wedding.WeddingId == id).Include(w => w.Guests).ThenInclude(w => w.User).SingleOrDefault();
                ViewBag.CurrWedding = CurrWedding;
                return View();
            }
            [HttpGet]
            [Route("/delete/{id}")]
            public IActionResult Delete(int id)
            {
                System.Console.WriteLine("*****************");
                System.Console.WriteLine(id);
                List<Guest> RemoveGuests = _context.Guests.Where(wedding => wedding.WeddingId == id).ToList();
                foreach( var RSVP in RemoveGuests)
                {
                    _context.Remove(RSVP);
                }
                _context.SaveChanges();


            int CurrentUser = (int)HttpContext.Session.GetInt32("user_id");
            Wedding RemoveWedding = _context.Weddings.Where(user => user.UserId == CurrentUser).Where(wedding => wedding.WeddingId == id).SingleOrDefault();
            _context.Remove(RemoveWedding);
            _context.SaveChanges();
            return RedirectToAction("dashboard");
            }
            [HttpGet]
            [Route("/unrsvp/{id}")]
            public IActionResult unrsvp(int id)
            {
                int CurrentUser = (int)HttpContext.Session.GetInt32("user_id");
                Guest UNRSVP = _context.Guests.Where(User => User.UserId == CurrentUser).Where(wedding => wedding.WeddingId == id).SingleOrDefault();
                _context.Remove(UNRSVP);
                _context.SaveChanges();
                return RedirectToAction("dashboard");

            }
            [HttpGet]
            [Route("/rsvp/{id}")]
            public IActionResult rsvp(int id)
            {
                int CurrentUser = (int)HttpContext.Session.GetInt32("user_id");
                Guest AddRSVP = new Guest 
                {
                    WeddingId = id,
                    UserId = CurrentUser
                };
                _context.Guests.Add(AddRSVP);
                _context.SaveChanges();
                return RedirectToAction("dashboard");

            }

        }
}

        