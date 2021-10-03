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
using Microsoft.AspNet.Identity;

namespace Bilinguals.Controllers
{
    [Authorize]
    public class SentencesController : Controller
    {
        private readonly ISentenceService _sentenceService;
        private readonly IDialogService _dialogService;
        private readonly IUserSentenceService _userSentenceService;

        public SentencesController(ISentenceService sentenceService, IDialogService dialogService, IUserSentenceService userSentenceService = null)
        {
            _sentenceService = sentenceService;
            _dialogService = dialogService;
            _userSentenceService = userSentenceService;
        }

        // GET: Sentences
        public ActionResult Index(int? pageIndex, string sortOrder, string searchText ) //Allow pageIndex Null ( int? ) 
        {
            int pageSize = 8;

            var sentences = _sentenceService.GetSentenceList(pageIndex ?? 1, pageSize, searchText, sortOrder);  // if pageIndex null else = 1 ( ?? )

            return View(sentences);
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
            var dialogs = _dialogService.GetAll();
            ViewBag.DialogId = new SelectList(dialogs, "Id", "Name");
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

            var dialogs = _dialogService.GetAll();
            ViewBag.DialogId = new SelectList(dialogs, "Id", "Name", sentence.DialogId);
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

            var dialogs = _dialogService.GetAll();
            ViewBag.DialogId = new SelectList(dialogs, "Id", "Name", sentence.DialogId);
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

            var dialogs = _dialogService.GetAll();
            ViewBag.DialogId = new SelectList(dialogs, "Id", "Name", sentence.DialogId);
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

        // GET:
        public ActionResult MyFeaturedSentence(int? pageIndex, string sortOrder)
        {
            var sentences = _userSentenceService.GetUserSentences(User.Identity.GetUserId(), pageIndex ?? 1, 8, sortOrder);
            return View();
        }

        #region JSON - AJAX REQUESTS
        public ActionResult SaveToMyFeaturedSentences(int sentenceId, int groupId, string returnUrl = null)
        {
            var userSentence = _userSentenceService.AddOrUpdateUserSentence(sentenceId, groupId, User.Identity.GetUserId());
            var groupName = userSentence.Group.Name;

            if (Request.IsAjaxRequest())
                return Json( new { id = userSentence.Id, name = groupName } , JsonRequestBehavior.AllowGet);

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromMyFeaturedSentence(int userSentenceId, string returnUrl = null)
        {
            _userSentenceService.Remove(userSentenceId);

            if (Request.IsAjaxRequest())
                return Json("", JsonRequestBehavior.AllowGet);

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index");
        }

        #endregion
    }
}
