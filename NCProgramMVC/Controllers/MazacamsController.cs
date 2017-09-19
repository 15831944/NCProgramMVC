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
    public class MazacamsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Mazacams
        public ActionResult Index()
        {
            var prodotti = db.Mazacams.OrderBy(s => s.Posizione).ToList();
            ViewBag.ProdottiCount = prodotti.Count();
            return View(prodotti);
        }

        //Memorizza l'ordine sortable dei prodotti
        [HttpPost]
        public ActionResult SortMazacam(string[] items)
        {
            foreach (var item in items.Select((value, i) => new { i, value }))
            {
                var newId = item.value.Substring(0, item.value.IndexOf("_"));
                var newPos = item.i;
                int NewId = Convert.ToInt32(newId);
                int NewPos = Convert.ToInt32(newPos);
                var sl = db.Mazacams.Where(s => s.Mazacam_Id == NewId).ToList();
                foreach (var item1 in sl)
                {
                    item1.Posizione = NewPos;
                    db.SaveChanges();
                }

            }
            return View();
        }


        // GET: Mazacams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mazacam mazacam = db.Mazacams.Find(id);
            if (mazacam == null)
            {
                return HttpNotFound();
            }
            return View(mazacam);
        }

        // GET: Mazacams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mazacams/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Mazacam_Id,Prodotto,Descrizione")] Mazacam mazacam)
        {
            if (ModelState.IsValid)
            {
                db.Mazacams.Add(mazacam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mazacam);
        }

        // GET: Mazacams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mazacam mazacam = db.Mazacams.Find(id);
            if (mazacam == null)
            {
                return HttpNotFound();
            }
            return View(mazacam);
        }

        // POST: Mazacams/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Mazacam_Id,Prodotto,Posizione", Exclude = "Descrizione")] Mazacam mazacam)
        {
            FormCollection collection = new FormCollection(Request.Unvalidated().Form);
            mazacam.Descrizione = collection["Descrizione"];
            if (ModelState.IsValid)
            {
                db.Entry(mazacam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mazacam);
        }

        public ActionResult EditImg(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mazacam prodotti = db.Mazacams.Find(id);
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
            Mazacam prodotti = db.Mazacams.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (extension == ".png")
                {
                    var filename = Server.MapPath("~/Content/Immagini/Mazacam/" + id + extension);
                    file.SaveAs(filename);
                    return RedirectToAction("Index", "Mazacams");
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


        // GET: Mazacams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mazacam mazacam = db.Mazacams.Find(id);
            if (mazacam == null)
            {
                return HttpNotFound();
            }
            return View(mazacam);
        }

        // POST: Mazacams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mazacam mazacam = db.Mazacams.Find(id);
            db.Mazacams.Remove(mazacam);
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
