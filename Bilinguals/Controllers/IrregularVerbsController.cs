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
    public class IrregularVerbsController : Controller
    {
        private BilingualDbContext db = new BilingualDbContext();

        // GET: IrregularVerbs
        public ActionResult Index()
        {
            return View(db.IrregularVerbs.ToList());
        }

        // GET: IrregularVerbs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IrregularVerb irregularVerb = db.IrregularVerbs.Find(id);
            if (irregularVerb == null)
            {
                return HttpNotFound();
            }
            return View(irregularVerb);
        }

        // GET: IrregularVerbs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IrregularVerbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Infinitive,SimplePast,PastParticiple,Mean,DateCreated,DateModified")] IrregularVerb irregularVerb)
        {
            if (ModelState.IsValid)
            {
                db.IrregularVerbs.Add(irregularVerb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(irregularVerb);
        }

        // GET: IrregularVerbs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IrregularVerb irregularVerb = db.IrregularVerbs.Find(id);
            if (irregularVerb == null)
            {
                return HttpNotFound();
            }
            return View(irregularVerb);
        }

        // POST: IrregularVerbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Infinitive,SimplePast,PastParticiple,Mean,DateCreated,DateModified")] IrregularVerb irregularVerb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(irregularVerb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(irregularVerb);
        }

        // GET: IrregularVerbs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IrregularVerb irregularVerb = db.IrregularVerbs.Find(id);
            if (irregularVerb == null)
            {
                return HttpNotFound();
            }
            return View(irregularVerb);
        }

        // POST: IrregularVerbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IrregularVerb irregularVerb = db.IrregularVerbs.Find(id);
            db.IrregularVerbs.Remove(irregularVerb);
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
