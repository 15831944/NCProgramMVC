﻿using System;
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
    public class MazacamDettsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MazacamDetts
        public ActionResult Index()
        {
            var mazacamDetts = db.MazacamDetts.Include(m => m.Prodotto);
            return View(mazacamDetts.ToList());
        }

        // GET: MazacamDetts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MazacamDett mazacamDett = db.MazacamDetts.Find(id);
            if (mazacamDett == null)
            {
                return HttpNotFound();
            }
            return View(mazacamDett);
        }

        // GET: MazacamDetts/Create
        public ActionResult Create()
        {
            ViewBag.Mazacam_Id = new SelectList(db.Mazacams, "Mazacam_Id", "Prodotto");
            return View();
        }

        // POST: MazacamDetts/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int prodotto, [Bind(Include = "MazacamDett_Id,Mazacam_Id,ProdottoDett,Descrizione")] MazacamDett mazacamDett)
        {
            if (ModelState.IsValid)
            {
                mazacamDett.Mazacam_Id = prodotto;
                db.MazacamDetts.Add(mazacamDett);
                db.SaveChanges();
                return RedirectToAction("Index", "Mazacams");
            }

            ViewBag.Mazacam_Id = new SelectList(db.Mazacams, "Mazacam_Id", "Prodotto", mazacamDett.Mazacam_Id);
            return View(mazacamDett);
        }

        // GET: MazacamDetts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MazacamDett mazacamDett = db.MazacamDetts.Find(id);
            if (mazacamDett == null)
            {
                return HttpNotFound();
            }
            ViewBag.Mazacam_Id = new SelectList(db.Mazacams, "Mazacam_Id", "Prodotto", mazacamDett.Mazacam_Id);
            return View(mazacamDett);
        }

        // POST: MazacamDetts/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MazacamDett_Id,Mazacam_Id,ProdottoDett", Exclude = "Descrizione")] MazacamDett mazacamDett)
        {
            FormCollection collection = new FormCollection(Request.Unvalidated().Form);
            mazacamDett.Descrizione = collection["Descrizione"];
            if (ModelState.IsValid)
            {
                db.Entry(mazacamDett).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Mazacams");
            }
            ViewBag.Mazacam_Id = new SelectList(db.Mazacams, "Mazacam_Id", "Prodotto", mazacamDett.Mazacam_Id);
            return View(mazacamDett);
        }

        // GET: MazacamDetts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MazacamDett mazacamDett = db.MazacamDetts.Find(id);
            if (mazacamDett == null)
            {
                return HttpNotFound();
            }
            return View(mazacamDett);
        }

        // POST: MazacamDetts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MazacamDett mazacamDett = db.MazacamDetts.Find(id);
            db.MazacamDetts.Remove(mazacamDett);
            db.SaveChanges();
            return RedirectToAction("Index", "Mazacams");
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
