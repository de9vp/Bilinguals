using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bilinguals.Data;
using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;

namespace Bilinguals.Controllers
{
    public class SentencesController : Controller
    {
        private readonly ISentenceService _sentenceService;

        public SentencesController(ISentenceService sentenceService)
        {
            _sentenceService = sentenceService;
        }

        // GET: Sentences
        public ActionResult Index(int? pageIndex, string searchText, string sortOrder) //Allow pageIndex Null ( int? ) 
        {
            int pageSize = 8;

            var sentences = _sentenceService.GetAll(pageIndex ?? 1, pageSize, searchText, sortOrder);  // if pageIndex null else = 1 ( ?? )

            return View(sentences.ToList());
        }

        // GET: Sentences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sentence = _sentenceService.GetById(id.Value);
            if (sentence == null)
            {
                return HttpNotFound();
            }
            return View(sentence);
        }

        // GET: Sentences/Create
        public ActionResult Create()
        {
            //ViewBag.DialogId = new SelectList(db.Dialogs, "Id", "Name");
            return View();
        }

        // POST: Sentences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sentence sentence)
        {
            if (ModelState.IsValid)
            {
                _sentenceService.Add(sentence);
                return RedirectToAction("Index");
            }

            //ViewBag.DialogId = new SelectList(db.Dialogs, "Id", "Name", sentence.DialogId);
            return View(sentence);
        }

        // GET: Sentences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sentence = _sentenceService.GetById(id.Value);
            if (sentence == null)
            {
                return HttpNotFound();
            }
            //ViewBag.DialogId = new SelectList(db.Dialogs, "Id", "Name", sentence.DialogId);
            return View(sentence);
        }

        // POST: Sentences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Sentence sentence, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                _sentenceService.Edit(sentence);

                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index");
            }
            //ViewBag.DialogId = new SelectList(db.Dialogs, "Id", "Name", sentence.DialogId);
            return View(sentence);
        }

        // GET: Sentences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sentence = _sentenceService.GetById(id.Value);
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
            _sentenceService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
