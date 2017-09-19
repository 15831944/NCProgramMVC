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

namespace NCProgramMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ServiziDettsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ServiziDetts
        public ActionResult Index()
        {
            var serviziDetts = db.ServiziDetts.Include(s => s.Servizio);
            return View(serviziDetts.ToList());
        }

        // GET: ServiziDetts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiziDett serviziDett = db.ServiziDetts.Find(id);
            if (serviziDett == null)
            {
                return HttpNotFound();
            }
            return View(serviziDett);
        }

        // GET: ServiziDetts/Create
        public ActionResult Create(int? servizio)
        {
            ViewBag.Servizio_Id = new SelectList(db.Servizis, "Servizio_Id", "Servizio");
            return View();
        }

        // POST: ServiziDetts/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int servizio, [Bind(Include = "ServizoDett_Id,Servizio_Id,ServizioDett,Posizione", Exclude = "Descrizione")] ServiziDett serviziDett)
        {
            FormCollection collection = new FormCollection(Request.Unvalidated().Form);
            serviziDett.Descrizione = collection["Descrizione"];
            if (ModelState.IsValid)
            {
                serviziDett.Servizio_Id = servizio;
                db.ServiziDetts.Add(serviziDett);
                db.SaveChanges();
                return RedirectToAction("Index", "Servizis");
            }

            ViewBag.Servizio_Id = new SelectList(db.Servizis, "Servizio_Id", "Servizio", serviziDett.Servizio_Id);
            return View(serviziDett);
        }

        // GET: ServiziDetts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiziDett serviziDett = db.ServiziDetts.Find(id);
            if (serviziDett == null)
            {
                return HttpNotFound();
            }
            ViewBag.Servizio_Id = new SelectList(db.Servizis, "Servizio_Id", "Servizio", serviziDett.Servizio_Id);
            return View(serviziDett);
        }

        // POST: ServiziDetts/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServizoDett_Id,Servizio_Id,ServizioDett,Posizione", Exclude = "Descrizione")] ServiziDett serviziDett)
        {
            FormCollection collection = new FormCollection(Request.Unvalidated().Form);
            serviziDett.Descrizione = collection["Descrizione"];
            if (ModelState.IsValid)
            {
                db.Entry(serviziDett).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Servizis");
            }
            ViewBag.Servizio_Id = new SelectList(db.Servizis, "Servizio_Id", "Servizio", serviziDett.Servizio_Id);
            return View(serviziDett);
        }

        // GET: EditOrder
        public ActionResult EditOrder(string servizio)
        {
            ViewBag.Servizio = servizio;
            var serviziDetts = db.ServiziDetts.Where(d=>d.Servizio.Servizio == servizio).OrderBy(d=>d.Posizione).Include(s => s.Servizio);
            return View(serviziDetts.ToList());
        }

        //Memorizza l'ordine sortable del dettagli dei servizi
        [HttpPost]
        public ActionResult SortServiziDett(string[] items)
        {
            foreach (var item in items.Select((value, i) => new { i, value }))
            {
                var newId = item.value.Substring(0, item.value.IndexOf("_"));
                var newPos = item.i;
                int NewId = Convert.ToInt32(newId);
                int NewPos = Convert.ToInt32(newPos);
                var sl = db.ServiziDetts.Where(s => s.ServizoDett_Id == NewId).ToList();
                foreach (var item1 in sl)
                {
                    item1.Posizione = NewPos;
                    db.SaveChanges();
                }

            }
            return RedirectToAction("Index", "Servizis");
        }

        // GET: ServiziDetts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiziDett serviziDett = db.ServiziDetts.Find(id);
            if (serviziDett == null)
            {
                return HttpNotFound();
            }
            return View(serviziDett);
        }

        // POST: ServiziDetts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiziDett serviziDett = db.ServiziDetts.Find(id);
            db.ServiziDetts.Remove(serviziDett);
            db.SaveChanges();
            return RedirectToAction("Index", "Servizis");
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
