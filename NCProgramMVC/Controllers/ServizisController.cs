using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NCProgramMVC.Models;
using System.Web.Helpers;
using System.IO;

namespace NCProgramMVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ServizisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Servizis
        public ActionResult Index()
        {
            var servizi = db.Servizis.OrderBy(s => s.Posizione).ToList();
            ViewBag.ServiziCount = servizi.Count();
            return View(servizi);
        }

        //Memorizza l'ordine sortable dei servizi
        [HttpPost]
        public ActionResult SortServizi(string[] items)
        {
            foreach (var item in items.Select((value, i) => new { i, value }))
            {
                var newId = item.value.Substring(0, item.value.IndexOf("_"));
                var newPos = item.i;
                int NewId = Convert.ToInt32(newId);
                int NewPos = Convert.ToInt32(newPos);
                var sl = db.Servizis.Where(s => s.Servizio_Id == NewId).ToList();
                foreach (var item1 in sl)
                {
                    item1.Posizione = NewPos;
                    db.SaveChanges();
                }

            }
            return View();
        }


        // GET: Servizis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servizi servizi = db.Servizis.Find(id);
            if (servizi == null)
            {
                return HttpNotFound();
            }
            return View(servizi);
        }

        // GET: Servizis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Servizis/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Servizio_Id,Servizio,Descrizione,Posizione")] Servizi servizi)
        {
            if (ModelState.IsValid)
            {
                db.Servizis.Add(servizi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(servizi);
        }

        // GET: Servizis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servizi servizi = db.Servizis.Find(id);
            if (servizi == null)
            {
                return HttpNotFound();
            }
            return View(servizi);
        }

        // POST: Servizis/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Servizio_Id,Servizio,Posizione", Exclude = "Descrizione")] Servizi servizi)
        {
            FormCollection collection = new FormCollection(Request.Unvalidated().Form);
            servizi.Descrizione = collection["Descrizione"];
            if (ModelState.IsValid)
            {
                db.Entry(servizi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(servizi);
        }


        public ActionResult EditImg(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servizi servizi = db.Servizis.Find(id);
            if (servizi == null)
            {
                return HttpNotFound();
            }
            return View(servizi);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditImg (int? id, HttpPostedFileBase file)
        {
            Servizi servizi = db.Servizis.Find(id);
            if (servizi == null)
            {
                return HttpNotFound();
            }

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (extension == ".png")
                {
                var filename = Server.MapPath("~/Content/Immagini/Servizi/" + id + extension);
                file.SaveAs(filename);
                return RedirectToAction("Index", "Servizis");
                }
                else
                {
                    ViewBag.Message = "Puoi usare solo file con estensione .png";
                    return View(servizi);
                }
            }
            else
            {
                ViewBag.Message = "Devi scegliere un file";
                return View(servizi);
            }
        }
        
        
        // GET: Servizis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servizi servizi = db.Servizis.Find(id);
            if (servizi == null)
            {
                return HttpNotFound();
            }
            return View(servizi);
        }

        // POST: Servizis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Servizi servizi = db.Servizis.Find(id);
            db.Servizis.Remove(servizi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
