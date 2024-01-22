using flight.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace flight.Controllers
{
    public class SanchitFlightController:Controller
    {
        public static Ace52024Context db;
        public SanchitFlightController(Ace52024Context _db)
        {
            db = _db;
        }

        // All Available Flights based on User Requirements
        public ActionResult GetAllFlight()
        {
            ViewBag.Username=HttpContext.Session.GetString("uname");
            ViewBag.Us=HttpContext.Session.GetInt32("cd");
            if(ViewBag.Username!=null){
                ViewBag.Src=HttpContext.Session.GetString("Src");
                ViewBag.Des=HttpContext.Session.GetString("Des");
                List<SanchitFlight> sanchitFlights=new List<SanchitFlight>();
                foreach(SanchitFlight i in db.SanchitFlights)
                {
                    if(i.Source==ViewBag.Src && i.Destination==ViewBag.Des)
                    {
                        sanchitFlights.Add(i);
                    }
                }
            return View(sanchitFlights);}
            else{
                return RedirectToAction("Login","Login");
            }
        }


        // Enter Source and Destination of User
        [HttpGet]
        public ActionResult GetTravel()
        {
            ViewBag.Username=HttpContext.Session.GetString("uname");
            if(ViewBag.Username!=null){
            var ss=db.SanchitFlights.Select(x=>x.Source).Distinct().ToList();
            ViewBag.Sources=new SelectList(ss);
            var sss=db.SanchitFlights.Select(x=>x.Destination).Distinct().ToList();
            ViewBag.Destinations=new SelectList(sss);
            return View();}
            else{
                return RedirectToAction("Login","Login");
            }
        }

        [HttpPost]
        public ActionResult GetTravel(SanchitFlight s)
        {
            ViewBag.Username=HttpContext.Session.GetString("uname");
            if(ViewBag.Username!=null){
                HttpContext.Session.SetString("Src", s.Source);
                HttpContext.Session.SetString("Des", s.Destination);
                return RedirectToAction("GetAllFlight");
            }
            else{
                return RedirectToAction("Login","Login");
            }
        }

        // Booking of Flight by User
        public ActionResult Book(int fid)
        {
            SanchitFlight s=db.SanchitFlights.Where(x=>x.Flightid==fid).SingleOrDefault();
            HttpContext.Session.SetString("ffid", (s.Flightid).ToString());
            return RedirectToAction("GetUserBooking","SanchitBooking");
        }

        // All flights to be seen by Admin
        public ActionResult GetAll()
        {
            ViewBag.Username=HttpContext.Session.GetString("uname");
            if(ViewBag.Username!=null){
            return View(db.SanchitFlights.ToList());}
            else{
                return RedirectToAction("Login","Login");
            }
        }
        
        // Edit option by Admin
        public ActionResult Edit(int id)
        {
            SanchitFlight e=db.SanchitFlights.Where(x=>x.Flightid==id).SingleOrDefault();
            return View(e);
        }

        [HttpPost]
        public ActionResult Edit(SanchitFlight e)
        {
            db.SanchitFlights.Update(e);
            db.SaveChanges();
            return RedirectToAction("GetAll");
        }
    }
}