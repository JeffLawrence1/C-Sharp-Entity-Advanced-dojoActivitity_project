using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using dojoActivity_project.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace dojoActivity_project.Controllers
{
    public class ActivityController : Controller
    {
        
            private YourContext _context;
            
                

            public ActivityController(YourContext context)
                
            {
                    
            _context = context;
                
            }
        // GET: /Home/
        [HttpGet]
        [Route("Dash")]
        public IActionResult Dash()
        {
            if(HttpContext.Session.GetInt32("UserID") == null){
                return RedirectToAction("Index", "Home");
            }
            int ID = (int)HttpContext.Session.GetInt32("UserID");
            List<Activity> A1 = _context.Activities.Include(x => x.Guest).ToList();
            User userName = _context.Users.Where(x => x.UserID == ID).SingleOrDefault();
            ViewBag.Name = userName;
            ViewBag.Activity = A1;
            ViewBag.ID = ID;
            return View("Dash");
        }

        [HttpGet]
        [Route("New")]
        public IActionResult New()
        {
            if(HttpContext.Session.GetInt32("UserID") == null){
                return RedirectToAction("Index", "Home");
            }
            return View("New");
        }

        [HttpGet]
        [Route("Activity/{ActivityID}")]
        public IActionResult Activity(int ActivityID)
        {
            if(HttpContext.Session.GetInt32("UserID") == null){
                return RedirectToAction("Index", "Home");
            }
            int ID = (int)HttpContext.Session.GetInt32("UserID");
            Activity AA = _context.Activities.Where(x => x.ActivityID == ActivityID).SingleOrDefault();
            List<Activity> guests = _context.Activities.Where(x => x.ActivityID == ActivityID).Include(x => x.Guest).ThenInclude(x => x.User).ToList();
            List<Activity> A1 = _context.Activities.Include(x => x.Guest).ToList();
            ViewBag.Activity1 = A1;
            ViewBag.Guest = guests;
            ViewBag.Activity = AA;
            ViewBag.ID = ID;
            ViewBag.ActID = ActivityID;
            return View("Activity");
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add(Activity newActivity, string Length){
            if(ModelState.IsValid){
                Activity newA = new Activity();
                newA.UserID = (int)HttpContext.Session.GetInt32("UserID");
                newA.Title = newActivity.Title;
                newA.Description = newActivity.Description;
                newA.Time = newActivity.Time;
                newA.Date = newActivity.Date;
                if(Length == "Minutes"){
                    newA.Duration = newActivity.Duration + "Minutes";
                }
                else if(Length == "Hours"){
                    newA.Duration = newActivity.Duration + "Hours";
                }
                else{
                    newA.Duration = newActivity.Duration + "Days";
                }
                
                newA.Participants = 0;
                newA.createdat = DateTime.Now;
                newA.updatedat = DateTime.Now;
                newA.Coordinator = HttpContext.Session.GetString("FirstName");
                _context.Activities.Add(newA);
                _context.SaveChanges();
                int ActivityID = _context.Activities.Last().ActivityID;
                return Redirect($"/Activity/{ActivityID}");
            }
            else{
                TempData["Error"] = "All fields are required, activies must be planned for the future!!";
                return RedirectToAction("New");
            }
        }
        [HttpGet]
        [Route("Delete/{ActivityID}")]
        public IActionResult Delete(int ActivityID){
            List<Guest> bad = _context.Guests.Where(x => x.ActivityID == ActivityID).ToList();
            foreach(var x in bad){
                _context.Guests.Remove(x);
                _context.SaveChanges();
            }
            Activity noFun = _context.Activities.Where(x => x.ActivityID == ActivityID).SingleOrDefault();
            _context.Activities.Remove(noFun);
            _context.SaveChanges();
            return RedirectToAction("Dash");
        }

        [HttpGet]
        [Route("Join/{ActivityID}")]
        public IActionResult Join(int ActivityID){
            Activity Participants = _context.Activities.Where(x => x.ActivityID == ActivityID).SingleOrDefault();

            Guest joiner = new Guest();
            int ID = (int)HttpContext.Session.GetInt32("UserID");
            joiner.UserID = ID;
            joiner.ActivityID = ActivityID;
            joiner.createdat = DateTime.Now;
            joiner.updatedat = DateTime.Now;
            Participants.Participants += 1;
        
            _context.Guests.Add(joiner);
            _context.SaveChanges();
            return RedirectToAction("Dash");
        }

        [HttpGet]
        [Route("Leave/{UserID}/{ActivityID}")]
        public IActionResult Leave(int UserID, int ActivityID){
            Activity Participants = _context.Activities.Where(x => x.ActivityID == ActivityID).SingleOrDefault();
            Guest leaver = _context.Guests.Where(x => x.ActivityID == ActivityID).Where(x => x.UserID == UserID).SingleOrDefault();
            Participants.Participants -= 1;
            _context.Guests.Remove(leaver);
            _context.SaveChanges();
            return RedirectToAction("Dash");
        }

    }
}
