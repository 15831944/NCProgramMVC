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
            var servizi = db.GruppoProdottis.Where(g => g.Prodotto == "Servizi" | g.Prodotto == "Mission" | g.Prodotto == "Storia").ToList();
            ViewBag.Servizi = servizi;
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
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";
            var prodotti = db.GruppoProdottis.ToList();
            ViewBag.Prodotti = prodotti;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Contact(InfoViewModels contatti)
        {

            if (ModelState.IsValid)
            {
                if(contatti.Privacy == false)
                {
                    ViewBag.Message = "Conferma la lettura del documento della privacy";
                    return View(contatti);
                }
                else
                {
                ViewBag.Message = "";
                MailMessage message = new MailMessage(
                    "webservice@ncprogram.it",
                    "cesare@cr-consult.eu,giuseppe.ferrari@ncprogram.it",
                    "Richiesta informazioni dal sito ncprogram.it",
                    "Il giorno " + DateTime.Now + "<br/><strong>" +
                    contatti.Nome + " " +
                    contatti.Cognome + "</strong> [" +
                    contatti.Email + "] " +
                    "<br/> ha inviato una richiesta di informazioni dal sito www.ncprogram.it<hr/>Prodotto: <strong>" +
                    contatti.Prodotto + 
                    "</strong><br/>Richiesta:<strong>" +
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
        public async Task<ActionResult> FormServizi(string servizio, InfoSerViewModels contatti)
        {

            if (ModelState.IsValid)
            {
                contatti.Servizio = servizio;
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

        public ActionResult FormProdotti(string prodotto)
        {
            ViewBag.Message = "Richiesta info per";
            ViewBag.Prodotto = prodotto;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> FormProdotti(string prodotto, InfoProViewModels contatti)
        {

            if (ModelState.IsValid)
            {
                contatti.Prodotto = prodotto;
                MailMessage message = new MailMessage(
                    "webservice@cr-consult.it",
                    "cesare@cr-consult.eu",
                    "Richiesta informazioni dal sito ncprogram.it",
                    "Il giorno " + DateTime.Now + "<br/><strong>" +
                    contatti.Nome + " " +
                    contatti.Cognome + "</strong> [" +
                    contatti.Email + "] " +
                    "<br/> ha inviato una richiesta di informazioni dal sito www.ncprogram.it per il prodotto <strong><em>" +
                    contatti.Prodotto +
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
            var gruppoProdotti = db.GruppoProdottis.Where(p => p.Prodotto == "Mission").ToList();
            return View(gruppoProdotti);
        }

        public ActionResult Storia()
        {
            var gruppoProdotti = db.GruppoProdottis.Where(p => p.Prodotto == "Storia").ToList();
            return View(gruppoProdotti);
        }

        public ActionResult Servizi()
        {
            var servizi = db.Servizis.OrderBy(s => s.Posizione).ToList();
            return View(servizi);
        }

        public ActionResult ServiziProdotti(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servizi serviziProdotti = db.Servizis.Find(id);
            if (serviziProdotti == null)
            {
                return HttpNotFound();
            }
            return View(serviziProdotti);
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
            var prodotti = db.Tdms.OrderBy(s => s.Posizione).ToList();
            var gruppoProdotti = db.GruppoProdottis.Where(p => p.Prodotto == "TDM").OrderBy(p=>p.Descrizione).ToList();
            ViewBag.GruppoProdotti = gruppoProdotti;
            return View(prodotti);
        }

        public ActionResult TdmProdotti(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tdm tdmProdotti = db.Tdms.Find(id);
            if (tdmProdotti == null)
            {
                return HttpNotFound();
            }
            return View(tdmProdotti);
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
            var prodotti = db.Cimcoes.OrderBy(s => s.Posizione).ToList();
            var gruppoProdotti = db.GruppoProdottis.Where(p => p.Prodotto == "CIMCO").OrderBy(p => p.Descrizione).ToList();
            ViewBag.GruppoProdotti = gruppoProdotti;
            return View(prodotti);
        }

        public ActionResult CimcoProdotti(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cimco cimcoProdotti = db.Cimcoes.Find(id);
            if (cimcoProdotti == null)
            {
                return HttpNotFound();
            }
            return View(cimcoProdotti);
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
            var prodotti = db.Mazacams.OrderBy(s => s.Posizione).ToList();
            var gruppoProdotti = db.GruppoProdottis.Where(p => p.Prodotto == "MAZACAM").OrderBy(p => p.Descrizione).ToList();
            ViewBag.GruppoProdotti = gruppoProdotti;
            return View(prodotti);
        }

        public ActionResult MazacamProdotti(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mazacam mazacamProdotti = db.Mazacams.Find(id);
            if (mazacamProdotti == null)
            {
                return HttpNotFound();
            }
            return View(mazacamProdotti);
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

        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult InfoCookie()
        {
            return View();
        }


    }
}