using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NCProgramMVC.Models;
using System.IO;

namespace NCProgramMVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class DocumentisController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Documentis
        public ActionResult Index()
        {
            var documenti = db.Documentis.ToList();
            ViewBag.DocumentiCount = documenti.Count();
            return View(documenti);
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

        [HttpPost]
        [Authorize]
        public ActionResult Download(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documenti documenti = db.Documentis.Find(id);
            if (ModelState.IsValid)
            {
                string fileName = documenti.Nomefile;
                byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/Documenti/" + fileName));
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
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
        public ActionResult Create([Bind(Include = "Documenti_Id,Titolo,Descrizione,Nomefile,Riservato")] Documenti documenti, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                    try
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        documenti.Nomefile = fileName;
                        db.Documentis.Add(documenti);
                        db.SaveChanges();
                        var path = Path.Combine(Server.MapPath("~/Content/Documenti/"), fileName);
                        file.SaveAs(path);
                        return RedirectToAction("Index", "Documentis");

                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                else
                {
                    ViewBag.Message = "Devi scegliere un file";
                    return View();
                }
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
                string fileName = documenti.Nomefile;
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
            var file = "~/Content/Documenti/" + documenti.Nomefile;
            System.IO.File.Delete(Server.MapPath(file));
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
