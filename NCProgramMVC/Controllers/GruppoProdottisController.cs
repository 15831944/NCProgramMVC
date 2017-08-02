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
    public class GruppoProdottisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GruppoProdottis
        public ActionResult Index()
        {
            return View(db.GruppoProdottis.ToList());
        }

        // GET: GruppoProdottis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GruppoProdotti gruppoProdotti = db.GruppoProdottis.Find(id);
            if (gruppoProdotti == null)
            {
                return HttpNotFound();
            }
            return View(gruppoProdotti);
        }

        // GET: GruppoProdottis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GruppoProdottis/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GruppoPodotti_Id,Prodotto,Descrizione,DescrizioneDett")] GruppoProdotti gruppoProdotti)
        {
            if (ModelState.IsValid)
            {
                db.GruppoProdottis.Add(gruppoProdotti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gruppoProdotti);
        }

        // GET: GruppoProdottis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GruppoProdotti gruppoProdotti = db.GruppoProdottis.Find(id);
            if (gruppoProdotti == null)
            {
                return HttpNotFound();
            }
            return View(gruppoProdotti);
        }

        // POST: GruppoProdottis/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GruppoPodotti_Id,Prodotto", Exclude = "Descrizione,DescrizioneDett")] GruppoProdotti gruppoProdotti)
        {
            FormCollection collection = new FormCollection(Request.Unvalidated().Form);
            gruppoProdotti.Descrizione = collection["Descrizione"];
            gruppoProdotti.DescrizioneDett = collection["DescrizioneDett"];
            if (ModelState.IsValid)
            {
                db.Entry(gruppoProdotti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gruppoProdotti);
        }

        // GET: GruppoProdottis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GruppoProdotti gruppoProdotti = db.GruppoProdottis.Find(id);
            if (gruppoProdotti == null)
            {
                return HttpNotFound();
            }
            return View(gruppoProdotti);
        }

        // POST: GruppoProdottis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GruppoProdotti gruppoProdotti = db.GruppoProdottis.Find(id);
            db.GruppoProdottis.Remove(gruppoProdotti);
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
