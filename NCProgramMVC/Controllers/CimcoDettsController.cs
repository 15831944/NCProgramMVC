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
    public class CimcoDettsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CimcoDetts
        public ActionResult Index()
        {
            var cimcoDetts = db.CimcoDetts.Include(c => c.Prodotto);
            return View(cimcoDetts.ToList());
        }

        // GET: CimcoDetts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CimcoDett cimcoDett = db.CimcoDetts.Find(id);
            if (cimcoDett == null)
            {
                return HttpNotFound();
            }
            return View(cimcoDett);
        }

        // GET: CimcoDetts/Create
        public ActionResult Create(int? prodotto)
        {
            ViewBag.Cimco_Id = new SelectList(db.Cimcoes, "Cimco_Id", "Prodotto");
            return View();
        }

        // POST: CimcoDetts/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int prodotto, [Bind(Include = "CimcoDett_Id,Cimco_Id,ProdottoDett", Exclude = "Descrizione")] CimcoDett cimcoDett)
        {
            FormCollection collection = new FormCollection(Request.Unvalidated().Form);
            cimcoDett.Descrizione = collection["Descrizione"];
            if (ModelState.IsValid)
            {
                cimcoDett.Cimco_Id = prodotto;
                db.CimcoDetts.Add(cimcoDett);
                db.SaveChanges();
                return RedirectToAction("Index", "Cimcoes");
            }

            ViewBag.Cimco_Id = new SelectList(db.Cimcoes, "Cimco_Id", "Prodotto", cimcoDett.Cimco_Id);
            return View(cimcoDett);
        }

        // GET: CimcoDetts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CimcoDett cimcoDett = db.CimcoDetts.Find(id);
            if (cimcoDett == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cimco_Id = new SelectList(db.Cimcoes, "Cimco_Id", "Prodotto", cimcoDett.Cimco_Id);
            return View(cimcoDett);
        }

        // POST: CimcoDetts/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CimcoDett_Id,Cimco_Id,ProdottoDett,Posizione", Exclude = "Descrizione")] CimcoDett cimcoDett)
        {
            FormCollection collection = new FormCollection(Request.Unvalidated().Form);
            cimcoDett.Descrizione = collection["Descrizione"];
            if (ModelState.IsValid)
            {
                db.Entry(cimcoDett).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Cimcoes");
            }
            ViewBag.Cimco_Id = new SelectList(db.Cimcoes, "Cimco_Id", "Prodotto", cimcoDett.Cimco_Id);
            return View(cimcoDett);
        }


        // GET: EditOrder
        public ActionResult EditOrder(string prodotto)
        {
            ViewBag.Prodotto = prodotto;
            var cimcoDetts = db.CimcoDetts.Where(d => d.Prodotto.Prodotto == prodotto).OrderBy(d => d.Posizione).Include(s => s.Prodotto);
            return View(cimcoDetts.ToList());
        }

        //Memorizza l'ordine sortable del dettaglio Cimco
        [HttpPost]
        public ActionResult SortCimcoDett(string[] items)
        {
            foreach (var item in items.Select((value, i) => new { i, value }))
            {
                var newId = item.value.Substring(0, item.value.IndexOf("_"));
                var newPos = item.i;
                int NewId = Convert.ToInt32(newId);
                int NewPos = Convert.ToInt32(newPos);
                var sl = db.CimcoDetts.Where(s => s.CimcoDett_Id == NewId).ToList();
                foreach (var item1 in sl)
                {
                    item1.Posizione = NewPos;
                    db.SaveChanges();
                }

            }
            return RedirectToAction("Index", "Cimcoes");
        }

        // GET: CimcoDetts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CimcoDett cimcoDett = db.CimcoDetts.Find(id);
            if (cimcoDett == null)
            {
                return HttpNotFound();
            }
            return View(cimcoDett);
        }

        // POST: CimcoDetts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CimcoDett cimcoDett = db.CimcoDetts.Find(id);
            db.CimcoDetts.Remove(cimcoDett);
            db.SaveChanges();
            return RedirectToAction("Index", "Cimcoes");
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
