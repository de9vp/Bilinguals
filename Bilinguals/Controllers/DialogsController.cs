using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bilinguals.Data;
using Bilinguals.Domain.Models;

namespace Bilinguals.Controllers
{
    public class DialogsController : Controller
    {
        private BilingualDbContext db = new BilingualDbContext();

        // GET: Dialogs
        public ActionResult Index()
        {
            return View(db.Dialogs.ToList());
        }

        // GET: Dialogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dialog dialog = db.Dialogs.Find(id);
            if (dialog == null)
            {
                return HttpNotFound();
            }
            return View(dialog);
        }

        // GET: Dialogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dialogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DateCreated,DateModified")] Dialog dialog)
        {
            if (ModelState.IsValid)
            {
                db.Dialogs.Add(dialog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dialog);
        }

        // GET: Dialogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dialog dialog = db.Dialogs.Find(id);
            if (dialog == null)
            {
                return HttpNotFound();
            }
            return View(dialog);
        }

        // POST: Dialogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Dialog dialog, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dialog).State = EntityState.Modified;
                db.SaveChanges();
                

                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index");
            }
            return View(dialog);
        }

        // GET: Dialogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dialog dialog = db.Dialogs.Find(id);
            if (dialog == null)
            {
                return HttpNotFound();
            }
            return View(dialog);
        }

        // POST: Dialogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dialog dialog = db.Dialogs.Find(id);
            db.Dialogs.Remove(dialog);
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
