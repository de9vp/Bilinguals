using System;
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
using Microsoft.AspNet.Identity;

namespace Bilinguals.Controllers
{
    public class GroupsController : Controller
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        // GET: Groups
        //public ActionResult Index()
        //{
        //    return View(db.Groups.ToList());
        //}

        // GET: Groups/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Group group = db.Groups.Find(id);
        //    if (group == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(group);
        //}

        // GET: Groups/Create
        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Group group, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                group.UserId = User.Identity.GetUserId();
                _groupService.Add(group);

                if (Request.IsAjaxRequest())
                    return Json(group, JsonRequestBehavior.AllowGet);

                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);                

                return RedirectToAction("Index");
            }
            if (Request.IsAjaxRequest())
                return Json(new { success = false, message = "Invalid" }, JsonRequestBehavior.AllowGet);

            return View(group);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = _groupService.GetById(id.Value);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Group group)
        {
            if (ModelState.IsValid)
            {
                _groupService.Edit(group);
                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = _groupService.GetById(id.Value);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _groupService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
