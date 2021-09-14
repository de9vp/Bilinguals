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
    public class SentencesController : Controller
    {
        private BilingualDbContext db = new BilingualDbContext();

        // GET: Sentences
        public ActionResult Index()
        {
            var sentences = db.Sentences.Include(s => s.Dialog);
            return View(sentences.ToList());
        }

        // GET: Sentences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sentence sentence = db.Sentences.Find(id);
            if (sentence == null)
            {
                return HttpNotFound();
            }
            return View(sentence);
        }

        // GET: Sentences/Create
        public ActionResult Create()
        {
            ViewBag.DialogId = new SelectList(db.Dialogs, "Id", "Name");
            return View();
        }

        // POST: Sentences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EnText,ViText,DialogId,SortOrder,DateCreated,DateModified")] Sentence sentence)
        {
            if (ModelState.IsValid)
            {
                db.Sentences.Add(sentence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DialogId = new SelectList(db.Dialogs, "Id", "Name", sentence.DialogId);
            return View(sentence);
        }

        // GET: Sentences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sentence sentence = db.Sentences.Find(id);
            if (sentence == null)
            {
                return HttpNotFound();
            }
            ViewBag.DialogId = new SelectList(db.Dialogs, "Id", "Name", sentence.DialogId);
            return View(sentence);
        }

        // POST: Sentences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EnText,ViText,DialogId,SortOrder,DateCreated,DateModified")] Sentence sentence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sentence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DialogId = new SelectList(db.Dialogs, "Id", "Name", sentence.DialogId);
            return View(sentence);
        }

        // GET: Sentences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sentence sentence = db.Sentences.Find(id);
            if (sentence == null)
            {
                return HttpNotFound();
            }
            return View(sentence);
        }

        // POST: Sentences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sentence sentence = db.Sentences.Find(id);
            db.Sentences.Remove(sentence);
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
