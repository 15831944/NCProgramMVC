using NCProgramMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace NCProgramMVC.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var slides = db.Slides.Where(s=>s.Pubblica == true).OrderBy(s => s.Posizione).ToList();
            ViewBag.SlidesCount = slides.Count();
            return View(slides);
        }

        [Authorize]
        public ActionResult IndexDoc()
        {
            if (User.IsInRole("Download"))
            {
                var documenti = db.Documentis.OrderBy(d => d.Titolo).ToList();
                ViewBag.DocumentiCount = documenti.Count();
                return View(documenti);
            }
            else
            {
                var documenti = db.Documentis.Where(d => d.Riservato != true).OrderBy(d => d.Titolo).ToList();
                ViewBag.DocumentiCount = documenti.Count();
                return View(documenti);
            }
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

        [HttpPost]
        public async Task<ActionResult> Contact(InfoViewModels contatti)
        {

            if (ModelState.IsValid)
            {
                MailMessage message = new MailMessage(
                    "webservice@cr-consult.it",
                    "cesare@cr-consult.eu,giuseppe.ferrari@ncprogram.it",
                    "Richiesta informazioni dal sito ncprogram.it",
                    "Il giorno " + DateTime.Now + "<br/><strong>" +
                    contatti.Nome + " " +
                    contatti.Cognome + "</strong> [" +
                    contatti.Email + "] " +
                    "<br/> ha inviato una richiesta di informazioni dal sito www.ncprogram.it<hr/>Richiesta: <strong>" +
                    contatti.Messaggio +
                    "</strong></li>"
                    );
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                }
                return RedirectToAction("FormOk", "Home");
            }
            return View(contatti);
        }

        public ActionResult FormServizi(string servizio)
        {
            ViewBag.Message = "Richiesta info per";
            ViewBag.Servizio = servizio;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> FormServizi(InfoSerViewModels contatti)
        {

            if (ModelState.IsValid)
            {
                MailMessage message = new MailMessage(
                    "webservice@cr-consult.it",
                    "cesare@cr-consult.eu",
                    "Richiesta informazioni dal sito ncprogram.it",
                    "Il giorno " + DateTime.Now + "<br/><strong>" +
                    contatti.Nome + " " +
                    contatti.Cognome + "</strong> [" +
                    contatti.Email + "] " +
                    "<br/> ha inviato una richiesta di informazioni dal sito www.ncprogram.it per il servizio <strong><em>" + 
                    contatti.Servizio +
                    "</strong></em><hr/>Richiesta: <strong>" +
                    contatti.Messaggio +
                    "</strong></li>"
                    );
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                }
                return RedirectToAction("FormOk", "Home");
            }
            return View(contatti);
        }

        public ActionResult FormOk()
        {
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
            var servizi = db.Servizis.OrderBy(s => s.Servizio).ToList();
            return View(servizi);
        }

        public ActionResult ServiziDett(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiziDett servizi = db.ServiziDetts.Find(id);
            if (servizi == null)
            {
                return HttpNotFound();
            }
            return View(servizi);
        }

        [Authorize(Roles ="Admin")]
        public ActionResult Prodotti()
        {
            return View();
        }

        public ActionResult ServiziList()
        {
            return View();
        }
        public ActionResult Software()
        {
            return View();
        }
        public ActionResult TDM()
        {
            var prodotti = db.Tdms.OrderBy(s => s.Prodotto).ToList();
            return View(prodotti);
        }

        public ActionResult TdmDett(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TdmDett prodotti = db.TdmDetts.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        public ActionResult CIMCO()
        {
            var prodotti = db.Cimcoes.OrderBy(s => s.Prodotto).ToList();
            return View(prodotti);
        }

        public ActionResult CimcoDett(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CimcoDett servizi = db.CimcoDetts.Find(id);
            if (servizi == null)
            {
                return HttpNotFound();
            }
            return View(servizi);
        }

        public ActionResult Mazac()
        {
            var prodotti = db.Mazacams.OrderBy(s => s.Prodotto).ToList();
            return View(prodotti);
        }

        public ActionResult MazacDett(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MazacamDett servizi = db.MazacamDetts.Find(id);
            if (servizi == null)
            {
                return HttpNotFound();
            }
            return View(servizi);
        }


    }
}