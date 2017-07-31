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
    public class TdmsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tdms
        public ActionResult Index()
        {
            var prodotti = db.Tdms.OrderBy(s => s.Prodotto).ToList();
            ViewBag.ProdottiCount = prodotti.Count();
            return View(prodotti);
        }

        // GET: Tdms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tdm tdm = db.Tdms.Find(id);
            if (tdm == null)
            {
                return HttpNotFound();
            }
            return View(tdm);
        }

        // GET: Tdms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tdms/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Tdm_Id,Prodotto,Descrizione")] Tdm tdm)
        {
            if (ModelState.IsValid)
            {
                db.Tdms.Add(tdm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tdm);
        }

        // GET: Tdms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tdm tdm = db.Tdms.Find(id);
            if (tdm == null)
            {
                return HttpNotFound();
            }
            return View(tdm);
        }

        // POST: Tdms/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Tdm_Id,Prodotto", Exclude = "Descrizione")] Tdm tdm)
        {
            FormCollection collection = new FormCollection(Request.Unvalidated().Form);
            tdm.Descrizione = collection["Descrizione"];
            if (ModelState.IsValid)
            {
                db.Entry(tdm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tdm);
        }

        public ActionResult EditImg(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tdm prodotti = db.Tdms.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditImg(int? id, HttpPostedFileBase file)
        {
            Tdm prodotti = db.Tdms.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (extension == ".png")
                {
                    var filename = Server.MapPath("~/Content/Immagini/Tdm/" + id + extension);
                    file.SaveAs(filename);
                    return RedirectToAction("Index", "Tdms");
                }
                else
                {
                    ViewBag.Message = "Puoi usare solo file con estensione .png";
                    return View(prodotti);
                }
            }
            else
            {
                ViewBag.Message = "Devi scegliere un file";
                return View(prodotti);
            }
        }




        // GET: Tdms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tdm tdm = db.Tdms.Find(id);
            if (tdm == null)
            {
                return HttpNotFound();
            }
            return View(tdm);
        }

        // POST: Tdms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tdm tdm = db.Tdms.Find(id);
            db.Tdms.Remove(tdm);
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
