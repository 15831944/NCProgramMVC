using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NCProgramMVC.Models;

namespace NCProgramMVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class DocumentisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Documentis
        public ActionResult Index()
        {
            return View(db.Documentis.ToList());
        }

        // GET: Documentis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documenti documenti = db.Documentis.Find(id);
            if (documenti == null)
            {
                return HttpNotFound();
            }
            return View(documenti);
        }

        // GET: Documentis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Documentis/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Documenti_Id,Titolo,Descrizione,Nomefile,Riservato")] Documenti documenti)
        {
            if (ModelState.IsValid)
            {
                db.Documentis.Add(documenti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(documenti);
        }

        // GET: Documentis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documenti documenti = db.Documentis.Find(id);
            if (documenti == null)
            {
                return HttpNotFound();
            }
            return View(documenti);
        }

        // POST: Documentis/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Documenti_Id,Titolo,Descrizione,Nomefile,Riservato")] Documenti documenti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documenti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(documenti);
        }

        // GET: Documentis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documenti documenti = db.Documentis.Find(id);
            if (documenti == null)
            {
                return HttpNotFound();
            }
            return View(documenti);
        }

        // POST: Documentis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Documenti documenti = db.Documentis.Find(id);
            db.Documentis.Remove(documenti);
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
