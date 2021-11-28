using Bilinguals.App;
using Bilinguals.Domain.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bilinguals.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISentenceService _sentenceService;
        private readonly IDialogService _dialogService;
        private readonly IUserService _userService;

        public HomeController(ISentenceService sentenceService, IDialogService dialogService, IUserService userService)
        {
            _sentenceService = sentenceService;
            _dialogService = dialogService;
            _userService = userService;
        }

        public ActionResult Index(int? sentenceIndex, int? dialogIndex,  string searchText, string sortOrder)
        {
            

            var userId = User.Identity.GetUserId();

            // get by sentence
            var sentences = _sentenceService.GetSentenceHome(sentenceIndex ?? 1, 7, searchText, sortOrder, userId);
            if (searchText == "")
            {
                return View(sentences);
            }
            // get by dialog
            if (searchText != null)
            {
                ViewBag.dialogs = _dialogService.GetDialogList(dialogIndex ?? 1, 7, searchText, sortOrder, userId);
            }

            return View(sentences);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // custom view update avatar
        [ChildActionOnly]
        public ActionResult LoginPartial()
        {
            var user = _userService.GetById(User.Identity.GetUserId());
            ViewBag.ImagePath = user?.image?.ImagePath ?? null;
            ViewBag.FullName = user?.FullName;
            return PartialView("_LoginPartial");
        }
    }

    //foreach (var item in listUser)
    //{
    //    var roleIds = item.Roles.Select(s => s.RoleId).ToList();
    //    var roleNames = _roleManager.Roles
    //        .Where(role => roleIds.Contains(role.Id))
    //        .Select(s => s.Name).ToList();

    //    item.StrRoleNames = string.Join(",", roleNames);
    //}

    // with 1 role
    //var roleIds = entityUser.Roles.Select(s => s.RoleId).ToList();
    //var oldRoleName = _roleManager.Roles.Where(x => roleIds.Contains(x.Id)).Select(x => x.Name).ToList();
    //foreach (var item in oldRoleName)
    //{
    //    _userManager.RemoveFromRole(entityUser.Id, item);
    //}
    //_userManager.AddToRole(entityUser.Id, userModel.RoleId);


}