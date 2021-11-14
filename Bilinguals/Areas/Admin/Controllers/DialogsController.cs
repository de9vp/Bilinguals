using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Bilinguals.Areas.Admin.Controllers
{
    [Authorize]
    public class DialogsController : Controller
    {
        private readonly IDialogService _dialogService;

        public DialogsController(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }
        // GET: Admin/Dialogs
        public ActionResult Index(int? page, string searchText)
        {
            int pageSize = 6;

            var listDialogs = _dialogService.GetDialogs(page ?? 1, pageSize, searchText);

            ViewBag.count = listDialogs.TotalItemCount.ToString();

            return View(listDialogs);
        }

        // GET: Admin/Dialogs/Details/5
        public ActionResult Details(int? id)
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

        // GET: Admin/Dialogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Dialogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dialog dialog)
        {
            if (ModelState.IsValid)
            {
                _dialogService.Add(dialog);
                return RedirectToAction("Index");
            }

            return View(dialog);
        }

        // GET: Admin/Dialogs/Edit/5
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

        // POST: Admin/Dialogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Dialog dialog, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                _dialogService.Edit(dialog);
                return RedirectToAction("Index");
            }
            return View(dialog);
        }

        // GET: Admin/Dialogs/Delete/5
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

        // POST: Admin/Dialogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _dialogService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}