using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NCProgramMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contatti di riferimento";

            return View();
        }

        public ActionResult Mission()
        {
            return View();
        }
        public ActionResult Storia()
        {
            return View();
        }
        public ActionResult Servizi()
        {
            return View();
        }
        public ActionResult Software()
        {
            return View();
        }
        public ActionResult TDM()
        {
            return View();
        }
        public ActionResult CIMCO()
        {
            return View();
        }
        public ActionResult Download()
        {
            return View();
        }
    }
}