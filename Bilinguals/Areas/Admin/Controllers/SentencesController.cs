using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Bilinguals.Areas.Admin.Controllers
{
    [Authorize]
    public class SentencesController : Controller
    {
        private readonly ISentenceService _sentenceService;
        private readonly IDialogService _dialogService;

        public SentencesController(ISentenceService sentenceService, IDialogService dialogService)
        {
            _sentenceService = sentenceService;
            _dialogService = dialogService;
        }
        // GET: Admin/Sentences
        public ActionResult Index(int? page, string sortOrder, string searchText)
        {
            int pageSize = 10;

            var listSentences = _sentenceService.GetSentenceList(page ?? 1, pageSize, searchText, sortOrder);

            ViewBag.count = listSentences.TotalItemCount.ToString();

            return View(listSentences);
        }

        // GET: Admin/Sentences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sentence sentence = _sentenceService.GetById(id.Value);
            if (sentence == null)
            {
                return HttpNotFound();
            }
            return View(sentence);
        }

        // GET: Admin/Sentences/Create
        public ActionResult Create()
        {
            var dialogs = _dialogService.GetAllDialogs();
            ViewBag.DialogId = new SelectList(dialogs, "Id", "Name");
            return View();
        }

        // POST: Admin/Sentences/Create
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

            var dialogs = _dialogService.GetAllDialogs();
            ViewBag.DialogId = new SelectList(dialogs, "Id", "Name", sentence.DialogId);
            return View(sentence);
        }

        // GET: Admin/Sentences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sentence sentence = _sentenceService.GetById(id.Value);
            if (sentence == null)
            {
                return HttpNotFound();
            }

            var dialogs = _dialogService.GetAllDialogs();
            ViewBag.DialogId = new SelectList(dialogs, "Id", "Name", sentence.DialogId);
            return View(sentence);
        }

        // POST: Admin/Sentences/Edit/5
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

            var dialogs = _dialogService.GetAllDialogs();
            ViewBag.DialogId = new SelectList(dialogs, "Id", "Name", sentence.DialogId);
            return View(sentence);
        }

        // GET: Admin/Sentences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sentence sentence = _sentenceService.GetById(id.Value);
            if (sentence == null)
            {
                return HttpNotFound();
            }
            return View(sentence);
        }

        // POST: Admin/Sentences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _sentenceService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}