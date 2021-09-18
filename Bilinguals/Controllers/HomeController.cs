using Bilinguals.Domain.Interfaces;
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

        public HomeController(ISentenceService sentenceService)
        {
            _sentenceService = sentenceService;
        }

        public ActionResult Index(int? pageIndex, string searchText, string sortOrder)
        {
            int pageSize = 5;

            var sentences = _sentenceService.GetSentenceHome(pageIndex ?? 1, pageSize, searchText, sortOrder);

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