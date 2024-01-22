using Microsoft.AspNetCore.Mvc;
using flight.Models;

namespace flight.Controllers
{
    public class LoginController:Controller
    {
        public static Ace52024Context db;
        private readonly ISession session;
        public LoginController(Ace52024Context _db,IHttpContextAccessor httpContextAccessor)
        {
            db = _db;
            session = httpContextAccessor.HttpContext.Session;
        }

        [HttpGet]

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(SanchitPassenger e)
        {
            db.SanchitPassengers.Add(e);
            db.SaveChanges();
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(SanchitPassenger s)
        {
            var result=(from  i in db.SanchitPassengers where i.Email==s.Email && i.Password==s.Password select i).SingleOrDefault();
            if(result!=null)
            {
                HttpContext.Session.SetString("uname", result.Cname);
                HttpContext.Session.SetString("cd", (result.Cid).ToString());
                if(s.Ltype=="Admin" && s.Ltype==result.Ltype){
                return RedirectToAction("GetAll","SanchitFlight");}
                else if(result.Ltype=="User" && s.Ltype==result.Ltype){
                    return RedirectToAction("GetTravel","SanchitFlight");
                }
                else
                {
                    return View();
                }
            }
            else{
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult Profile()
        {
            ViewBag.Username=HttpContext.Session.GetString("uname");
            if(ViewBag.Username!=null){
                SanchitPassenger e = db.SanchitPassengers.Where(x => x.Cid == Int32.Parse(HttpContext.Session.GetString("cd"))).SingleOrDefault();
            return View(e);}
            else{
                return RedirectToAction("Login","Login");
            }
        }
    }
}