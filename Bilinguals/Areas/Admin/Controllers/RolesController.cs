using Bilinguals.Domain.Models;
using Bilinguals.Services;
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
    public class RolesController : Controller
    {
        private readonly ApplicationRoleManager _roleManager;

        public RolesController(ApplicationRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        // GET: Admin/Roles
        public ActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        // GET: Admin/Roles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationRole applicationRole = _roleManager.FindById(id);
            if (applicationRole == null)
            {
                return HttpNotFound();
            }
            return View(applicationRole);
        }

        // GET: Admin/Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ApplicationRole applicationRole)
        {
            if (_roleManager.RoleExists(applicationRole.Name))
                ModelState.AddModelError("Name", $"{applicationRole.Name} already exists.");

            if (ModelState.IsValid)
            {
                _roleManager.Create(applicationRole);

                return RedirectToAction("Index");
            }

            return View(applicationRole);
        }

        // GET: Admin/Roles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationRole applicationRole = _roleManager.FindById(id);
            if (applicationRole == null)
            {
                return HttpNotFound();
            }
            return View(applicationRole);
        }

        // POST: Admin/Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationRole applicationRole)
        {
            if (ModelState.IsValid)
            {
                _roleManager.Update(applicationRole);
                return RedirectToAction("Index");
            }
            return View(applicationRole);
        }

        // GET: Admin/Roles/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationRole applicationRole = _roleManager.FindById(id);
            if (applicationRole == null)
            {
                return HttpNotFound();
            }
            return View(applicationRole);
        }

        // POST: Admin/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationRole applicationRole = _roleManager.FindById(id);
            _roleManager.Delete(applicationRole);
            return RedirectToAction("Index");
        }
    }
}