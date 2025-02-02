﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bilinguals.App;
using Bilinguals.Data;
using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;
using Bilinguals.Models;
using Microsoft.AspNet.Identity;

namespace Bilinguals.Controllers
{
    [Authorize]
    public class DialogsController : Controller
    {
        private readonly IDialogService _dialogService;
        private readonly IUserDialogService _userDialogService;
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        

        public DialogsController(IDialogService dialogService, IUserDialogService userDialogService, ICommentService commentService, IUserService userService)
        {
            _dialogService = dialogService;
            _userDialogService = userDialogService;
            _commentService = commentService;
            _userService = userService;
        }

        // GET: Dialogs
        public ActionResult Index(int? pageIndex, string searchText, string sortOrder)
        {
            int pageSize = 9;

            var userId = User.Identity.GetUserId();

            var dialogs = _dialogService.GetDialogList(pageIndex ?? 1, pageSize, searchText, sortOrder, userId);

            return View(dialogs);
        }

        // GET: Dialogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dialog dialog = _dialogService.GetDialogDetailAndSentences(id.Value, User.Identity.GetUserId());
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
        public ActionResult Create(Dialog dialog)
        {
            if (ModelState.IsValid)
            {
                _dialogService.Add(dialog);
                return RedirectToAction("Index", "Dialogs");
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
            Dialog dialog = _dialogService.GetById(id.Value);
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
                _dialogService.Edit(dialog);
                

                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Dialogs");
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
            Dialog dialog = _dialogService.GetById(id.Value);
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
            _dialogService.Delete(id);
            return RedirectToAction("Index", "Dialogs");
        }

        public ActionResult MyDialog(int? pageIndex, string sortOrder)
        {
            var dialogs = _userDialogService.GetUserDialogs(User.Identity.GetUserId(), pageIndex ?? 1, 4, sortOrder);

            return View(dialogs);
        }

        #region JSON - AJAX REQUESTS
        public ActionResult SaveToMyDialogs(int dialogId, string returnUrl = null)
        {
            var userDialog = _userDialogService.AddUserDialog(dialogId, User.Identity.GetUserId());

            if (Request.IsAjaxRequest())
                return Json(userDialog.Id, JsonRequestBehavior.AllowGet);

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index");
        }

        public ActionResult MarkLearned(int userDialogId, string returnUrl = null)
        {
            _userDialogService.MarkLearned(userDialogId);

            if (Request.IsAjaxRequest())
                return Json("", JsonRequestBehavior.AllowGet);

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromMyDialogs(int userDialogId, string returnUrl = null)
        {
            _userDialogService.Delete(userDialogId);

            if (Request.IsAjaxRequest())
                return Json("", JsonRequestBehavior.AllowGet);

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index");
        }

        public ActionResult DialogComment(CommentViewModel comment)
        {
            var user = _userService.GetById(User.Identity.GetUserId());
            var commentEntity = new Comment
            {
                Text = comment.Text,
                UserId = User.Identity.GetUserId(),
                TimeStamp = DateTime.Now,
                DialogId = comment.DialogId,
            };
            var newComment = _commentService.Add(commentEntity);
            newComment.UserFullname = user.FullName;
            return Json(JsonResultHelper.MapCommentJson(newComment), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetComment(int? dialogId)
        {
            var comments = _commentService.GetByDialogId(dialogId.Value);
            
            foreach (var comment in comments)
            {
                var user = _userService.GetById(comment.UserId);
                comment.UserFullname = user.FullName;
                comment.TimeConvert = comment.TimeStamp.ToString("dd/MM/yy a't' HH:mm");
            }
            return Json(comments, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteComment(int? commentId)
        {
            if (commentId != null)
            {
                _commentService.Delete(commentId.Value);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditComment(int? commentId, string text)
        {
            var commentEntity = _commentService.GetById(commentId.Value);
            commentEntity.Text = text;
            var newComment = _commentService.Edit(commentEntity);
            return Json(JsonResultHelper.MapCommentJson(newComment), JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
