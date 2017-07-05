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
using System.Web.Helpers;

namespace NCProgramMVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class SlidesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Slides
        public ActionResult Index()
        {
            var slide = db.Slides.ToList();
            ViewBag.SlideCount = slide.Count();
            return View(slide);
        }

        // GET: Slides/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // GET: Slides/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Slides/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Slide_Id,Titolo,Sottotitolo,Sfondo,Posizione,Pubblica", Exclude = "Descrizione")] Slide slide, HttpPostedFileBase file)
        {
            FormCollection collection = new FormCollection(Request.Unvalidated().Form);
            slide.Descrizione = collection["Descrizione"];
            if (ModelState.IsValid)
            {
                if (file != null)
                    try
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        slide.Sfondo = fileName;
                        db.Slides.Add(slide);
                        db.SaveChanges();

                        var path = Path.Combine(Server.MapPath("~/Content/Immagini/Slides/"), fileName);
                        System.Web.Helpers.WebImage img = new System.Web.Helpers.WebImage(file.InputStream);
                        var larghezza = img.Width;
                        var altezza = img.Height;
                        var rapportoO = larghezza / altezza;
                        var rapportoV = altezza / larghezza;
                        if (altezza > 1900 | larghezza > 1900)
                        {
                            if (rapportoO >= 1)
                            {
                                ViewBag.Message = "Attendi la fine del download...";
                                img.Resize(1900, 1900 / rapportoO);
                                img.Save(path);
                                ViewBag.Message = "Download immagine orizzontale avvenuto con successo. Dimensione immagine originale: larghezza " + larghezza + " Altezza " + altezza;
                            }
                            else
                            {
                                ViewBag.Message = "Attendi la fine del download...";
                                img.Resize(800 / rapportoV, 800);
                                img.Save(path);
                                ViewBag.Message = "Download immagine verticale avvenuto con successo. Dimensione immagine: larghezza " + larghezza + "Altezza" + altezza;
                            }
                        }
                        else
                        {
                            if (rapportoO >= 1)
                            {
                                ViewBag.Message = "Attendi la fine del download...";
                                img.Save(path);
                                ViewBag.Message = "Download immagine orizzontale avvenuto con successo. Dimensione immagine originale: larghezza " + larghezza + " Altezza " + altezza;
                            }
                            else
                            {
                                ViewBag.Message = "Attendi la fine del download...";
                                img.Save(path);
                                ViewBag.Message = "Download immagine verticale avvenuto con successo. Dimensione immagine: larghezza " + larghezza + "Altezza" + altezza;
                            }
                        }
                        return RedirectToAction("Index", "Slides");

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


                return RedirectToAction("Index");
            }

            return View(slide);
        }

        // GET: Slides/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // POST: Slides/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Slide_Id,Titolo,Sottotitolo,Sfondo,Posizione,Pubblica", Exclude = "Descrizione")] Slide slide, HttpPostedFileBase file)
        {
            FormCollection collection = new FormCollection(Request.Unvalidated().Form);
            slide.Descrizione = collection["Descrizione"];
            if (ModelState.IsValid)
            {

                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    slide.Sfondo = fileName;
                    var path = Path.Combine(Server.MapPath("~/Content/Immagini/Slides/"), fileName);
                    System.Web.Helpers.WebImage img = new System.Web.Helpers.WebImage(file.InputStream);
                    var larghezza = img.Width;
                    var altezza = img.Height;
                    var rapportoO = larghezza / altezza;
                    var rapportoV = altezza / larghezza;
                    if (altezza > 1900 | larghezza > 1900)
                    {
                        if (rapportoO >= 1)
                        {
                            ViewBag.Message = "Attendi la fine del download...";
                            img.Resize(1900, 1900 / rapportoO);
                            img.Save(path);
                            ViewBag.Message = "Download immagine orizzontale avvenuto con successo. Dimensione immagine originale: larghezza " + larghezza + " Altezza " + altezza;
                        }
                        else
                        {
                            ViewBag.Message = "Attendi la fine del download...";
                            img.Resize(800 / rapportoV, 800);
                            img.Save(path);
                            ViewBag.Message = "Download immagine verticale avvenuto con successo. Dimensione immagine: larghezza " + larghezza + "Altezza" + altezza;
                        }
                    }
                    else
                    {
                        if (rapportoO >= 1)
                        {
                            ViewBag.Message = "Attendi la fine del download...";
                            img.Save(path);
                            ViewBag.Message = "Download immagine orizzontale avvenuto con successo. Dimensione immagine originale: larghezza " + larghezza + " Altezza " + altezza;
                        }
                        else
                        {
                            ViewBag.Message = "Attendi la fine del download...";
                            img.Save(path);
                            ViewBag.Message = "Download immagine verticale avvenuto con successo. Dimensione immagine: larghezza " + larghezza + "Altezza" + altezza;
                        }
                    }
                    db.Entry(slide).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                db.Entry(slide).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
                }
            }
            return View(slide);
        }

        // GET: Slides/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // POST: Slides/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slide slide = db.Slides.Find(id);
            db.Slides.Remove(slide);
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
