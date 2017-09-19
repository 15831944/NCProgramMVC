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
    public class TdmDettsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TdmDetts
        public ActionResult Index()
        {
            var tdmDetts = db.TdmDetts.Include(t => t.Prodotto);
            return View(tdmDetts.ToList());
        }

        // GET: TdmDetts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TdmDett tdmDett = db.TdmDetts.Find(id);
            if (tdmDett == null)
            {
                return HttpNotFound();
            }
            return View(tdmDett);
        }

        // GET: TdmDetts/Create
        public ActionResult Create(int? prodotto)
        {
            ViewBag.Tdm_Id = new SelectList(db.Tdms, "Tdm_Id", "Prodotto");
            return View();
        }

        // POST: TdmDetts/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int prodotto, [Bind(Include = "TdmDett_Id,Tdm_Id,ProdottoDett", Exclude = "Descrizione")] TdmDett tdmDett)
        {
            FormCollection collection = new FormCollection(Request.Unvalidated().Form);
            tdmDett.Descrizione = collection["Descrizione"];
            if (ModelState.IsValid)
            {
                tdmDett.Tdm_Id = prodotto;
                db.TdmDetts.Add(tdmDett);
                db.SaveChanges();
                return RedirectToAction("Index", "Tdms");
            }

            ViewBag.Tdm_Id = new SelectList(db.Tdms, "Tdm_Id", "Prodotto", tdmDett.Tdm_Id);
            return View(tdmDett);
        }

        // GET: TdmDetts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TdmDett tdmDett = db.TdmDetts.Find(id);
            if (tdmDett == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tdm_Id = new SelectList(db.Tdms, "Tdm_Id", "Prodotto", tdmDett.Tdm_Id);
            return View(tdmDett);
        }

        // POST: TdmDetts/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TdmDett_Id,Tdm_Id,ProdottoDett,Posizione", Exclude = "Descrizione")] TdmDett tdmDett)
        {
            FormCollection collection = new FormCollection(Request.Unvalidated().Form);
            tdmDett.Descrizione = collection["Descrizione"];
            if (ModelState.IsValid)
            {
                db.Entry(tdmDett).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Tdms");
            }
            ViewBag.Tdm_Id = new SelectList(db.Tdms, "Tdm_Id", "Prodotto", tdmDett.Tdm_Id);
            return View(tdmDett);
        }

        // GET: EditOrder
        public ActionResult EditOrder(string prodotto)
        {
            ViewBag.Prodotto = prodotto;
            var tdmDetts = db.TdmDetts.Where(d => d.Prodotto.Prodotto == prodotto).OrderBy(d => d.Posizione).Include(s => s.Prodotto);
            return View(tdmDetts.ToList());
        }

        //Memorizza l'ordine sortable del dettaglio TDM
        [HttpPost]
        public ActionResult SortTdmDett(string[] items)
        {
            foreach (var item in items.Select((value, i) => new { i, value }))
            {
                var newId = item.value.Substring(0, item.value.IndexOf("_"));
                var newPos = item.i;
                int NewId = Convert.ToInt32(newId);
                int NewPos = Convert.ToInt32(newPos);
                var sl = db.TdmDetts.Where(s => s.TdmDett_Id == NewId).ToList();
                foreach (var item1 in sl)
                {
                    item1.Posizione = NewPos;
                    db.SaveChanges();
                }

            }
            return RedirectToAction("Index", "Tdms");
        }

        // GET: TdmDetts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TdmDett tdmDett = db.TdmDetts.Find(id);
            if (tdmDett == null)
            {
                return HttpNotFound();
            }
            return View(tdmDett);
        }

        // POST: TdmDetts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TdmDett tdmDett = db.TdmDetts.Find(id);
            db.TdmDetts.Remove(tdmDett);
            db.SaveChanges();
            return RedirectToAction("Index", "Tdms");
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
