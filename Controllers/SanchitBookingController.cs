using flight.Models;
using Microsoft.AspNetCore.Mvc;

namespace flight.Controllers
{
    public class SanchitBookingController:Controller
    {
        public static Ace52024Context db;
        public SanchitBookingController(Ace52024Context _db)
        {
            db = _db;
        }

        // Bookings done by Admin
        public ActionResult GetBookingDetails()
        {
            ViewBag.Username=HttpContext.Session.GetString("uname");
            if(ViewBag.Username!=null){
            return View(db.SanchitBookings.ToList());}
            else{
                return RedirectToAction("Login","Login");
            }
        }

        // Booking to be done by User
        public ActionResult GetUserBooking()
        {
            ViewBag.Username=HttpContext.Session.GetString("uname");
            if(ViewBag.Username!=null){
            return View();}
            else{
                return RedirectToAction("Login","Login");
            }
        }
        [HttpPost]
        public ActionResult GetUserBooking(SanchitBooking e)
        {
            ViewBag.Username=HttpContext.Session.GetString("uname");
            if(ViewBag.Username!=null){
                e.BookingDate=DateTime.Now;
                e.Source=HttpContext.Session.GetString("Src");
                e.Destination=HttpContext.Session.GetString("Des");
                e.Flightid=Int32.Parse(HttpContext.Session.GetString("ffid"));
                foreach(SanchitFlight i in db.SanchitFlights)
                {
                    if(i.Flightid==e.Flightid)
                    {
                        e.TotalPrice=e.NoPassengers*i.Price;
                    }
                }
                e.Cid= Int32.Parse(HttpContext.Session.GetString("cd"));
                
                db.SanchitBookings.Add(e);
                db.SaveChanges();
                return RedirectToAction("GetBook");
            }
            else{
                return RedirectToAction("Login","Login");
            }
        }

        // Bookings done by User
        public ActionResult GetBook()
        {
            ViewBag.Username=HttpContext.Session.GetString("uname");
            if(ViewBag.Username!=null){
                List<SanchitBooking> ll=new List <SanchitBooking>();
                foreach(SanchitBooking i in db.SanchitBookings)
                {
                    if(i.Cid==Int32.Parse(HttpContext.Session.GetString("cd")))
                    {
                        ll.Add(i);
                    }
                }

            return View(ll);}
            else{
                return RedirectToAction("Login","Login");
            }
        }

        // Cancellation to be done by User
        public ActionResult Cancel(int id)
        {
            SanchitBooking e=db.SanchitBookings.Where(x=>x.Bookingid==id).SingleOrDefault();
            return View(e);
        }

        [HttpPost]
        [ActionName("Cancel")]
        public ActionResult CancelUpdated(int id)
        {
            SanchitBooking e=db.SanchitBookings.Where(x=>x.Bookingid==id).SingleOrDefault();
            db.SanchitBookings.Remove(e);
            db.SaveChanges();
            return RedirectToAction("GetBook");
        }
        
    }
}