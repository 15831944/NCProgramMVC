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
    public class CimcoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cimcoes
        public ActionResult Index()
        {
            var prodotti = db.Cimcoes.OrderBy(s => s.Prodotto).ToList();
            ViewBag.ProdottiCount = prodotti.Count();
            return View(prodotti);
        }

        // GET: Cimcoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cimco cimco = db.Cimcoes.Find(id);
            if (cimco == null)
            {
                return HttpNotFound();
            }
            return View(cimco);
        }

        // GET: Cimcoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cimcoes/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cimco_Id,Prodotto,Descrizione")] Cimco cimco)
        {
            if (ModelState.IsValid)
            {
                db.Cimcoes.Add(cimco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cimco);
        }

        // GET: Cimcoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cimco cimco = db.Cimcoes.Find(id);
            if (cimco == null)
            {
                return HttpNotFound();
            }
            return View(cimco);
        }

        // POST: Cimcoes/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Cimco_Id,Prodotto", Exclude = "Descrizione")] Cimco cimco)
        {
            FormCollection collection = new FormCollection(Request.Unvalidated().Form);
            cimco.Descrizione = collection["Descrizione"];
            if (ModelState.IsValid)
            {
                db.Entry(cimco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cimco);
        }

        public ActionResult EditImg(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cimco prodotti = db.Cimcoes.Find(id);
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
            Cimco prodotti = db.Cimcoes.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (extension == ".png")
                {
                    var filename = Server.MapPath("~/Content/Immagini/Cimco/" + id + extension);
                    file.SaveAs(filename);
                    return RedirectToAction("Index", "Cimcoes");
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


        // GET: Cimcoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cimco cimco = db.Cimcoes.Find(id);
            if (cimco == null)
            {
                return HttpNotFound();
            }
            return View(cimco);
        }

        // POST: Cimcoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cimco cimco = db.Cimcoes.Find(id);
            db.Cimcoes.Remove(cimco);
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
