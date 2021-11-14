using Bilinguals.Areas.Admin.Models;
using Bilinguals.Domain.Models;
using Bilinguals.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Bilinguals.Areas.Admin.Controllers
{
    [Authorize]
    public class UserManagerController : Controller
    {
        private readonly ApplicationRoleManager _roleManager;
        private readonly ApplicationUserManager _userManager;

        public UserManagerController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: Admin/_userManager
        public ActionResult Index()
        {
            var listUser = _userManager.Users.ToList();
            return View(listUser);
        }

        // GET: Admin/_userManager/Edit
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = _userManager.FindById(id);
            var userModel = new UserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Id = id,
                Password = user.PasswordHash
            };

            PopulateSelectedRoleData(user); //Create a group of check boxes

            if (user == null)
                return HttpNotFound("User not found");
            return View(userModel);
        }

        // POST: Admin/_userManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserViewModel userModel, string[] selectedRoles, string returnUrl = null)
        {
            var entityUser = await _userManager.FindByIdAsync(userModel.Id);
            if (ModelState.IsValid)
            {
                entityUser.FirstName = userModel.FirstName;
                entityUser.LastName = userModel.LastName;
                entityUser.UserName = userModel.UserName;
                entityUser.PhoneNumber = userModel.PhoneNumber;
                entityUser.Email = userModel.Email;
                entityUser.DateOfBirth = userModel.DateOfBirth;
                entityUser.PasswordHash = userModel.Password;

                UpdateUserRoles(selectedRoles, entityUser);

                await _userManager.UpdateAsync(entityUser);

                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index");
            }
            PopulateSelectedRoleData(entityUser);
            return View(userModel);
        }

        // GET: Admin/_userManager/ChangePassword
        public ActionResult ChangePassword(string id)
        {
            if (string.IsNullOrEmpty(id))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = _userManager.FindById(id);
            var userModel = new UserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Id = id
            };


            if (user == null)
                return HttpNotFound("User not found");
            return View(userModel);
        }

        // POST: Admin/_userManager/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(UserViewModel userModel, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var entityUser = await _userManager.FindByIdAsync(userModel.Id);

                //reference 1 : 1
                entityUser.FirstName = userModel.FirstName;
                entityUser.LastName = userModel.LastName;
                entityUser.UserName = userModel.UserName;
                entityUser.PhoneNumber = userModel.PhoneNumber;
                entityUser.Email = userModel.Email;
                entityUser.DateOfBirth = userModel.DateOfBirth;

                entityUser.PasswordHash = _userManager.PasswordHasher.HashPassword(userModel.Password);

                await _userManager.UpdateAsync(entityUser);

                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index");
            }
            return View(userModel);
        }


        // GET: Admin/_userManager/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            var user = new ApplicationUser();
            PopulateSelectedRoleData(user); //Create a group of check boxes
            return View();
        }

        //POST: Admin/_userManager/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserViewModel model, string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var entityUser = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };

                var result = await _userManager.CreateAsync(entityUser, model.Password);

                UpdateUserRoles(selectedRoles, entityUser);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }

            var user = new ApplicationUser();
            PopulateSelectedRoleData(user);
            return View(model);
        }

        // GET: Admin/_userManager/Delete
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = _userManager.FindById(id);

            if (user == null)
                return HttpNotFound("User not found");

            return View(user);
        }

        //POST: Admin/_userManager/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(ApplicationUser user, string returnUrl = null)
        {
            var roleIds = await _userManager.GetRolesAsync(user.Id);
            var userEntity = await _userManager.FindByIdAsync(user.Id);

            foreach (var login in user.Logins)
                await _userManager.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));

            foreach (var item in roleIds)
                await _userManager.RemoveFromRoleAsync(user.Id, item);

            await _userManager.DeleteAsync(userEntity);

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index");
        }


        // GET: Admin/_userManager/Details
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = _userManager.FindById(id);

            if (user == null)
                return HttpNotFound("User not found");

            return View(user);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            var AuthenticationManager = HttpContext.GetOwinContext().Authentication;
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Dashboard");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        #region Add Roles to User 
        private void PopulateSelectedRoleData(ApplicationUser user)
        {
            //var allRole = _roleManager.GetAll();
            var allRole = _roleManager.Roles.ToList();

            var userRoles = new HashSet<string>(user.Roles.Select(c => c.RoleId));

            var viewModel = new List<SelectRoleData>();

            foreach (var role in allRole)
            {
                viewModel.Add(new SelectRoleData
                {
                    RoleId = role.Id,
                    Name = role.Name,
                    Selected = userRoles.Contains(role.Id)
                });
            }
            ViewBag.Roles = viewModel;
        }

        private void UpdateUserRoles(string[] selectedRoles, ApplicationUser userToUpdate)
        {
            if (selectedRoles == null)
            {
                var roles = _userManager.GetRoles(userToUpdate.Id);
                _userManager.RemoveFromRoles(userToUpdate.Id, roles.ToArray());  // Remove all roles
                return;
            }

            var selectedRolesHS = new HashSet<string>(selectedRoles);

            var userRoles = new HashSet<string>(userToUpdate.Roles.Select(c => c.RoleId));

            var allRole = _roleManager.Roles.ToList();

            foreach (var role in allRole)
            {
                if (selectedRolesHS.Contains(role.Id))
                {
                    if (!userRoles.Contains(role.Id))
                    {
                        _userManager.AddToRole(userToUpdate.Id, role.Name);
                    }
                }
                else
                {
                    if (userRoles.Contains(role.Id))
                    {
                        _userManager.RemoveFromRole(userToUpdate.Id, role.Name);
                    }
                }
            }
        }
        #endregion
    }
}