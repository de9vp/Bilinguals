using Bilinguals.Domain.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bilinguals.Controllers
{
    public class LearnController : Controller
    {
        private readonly IDialogService _dialogService;
        private readonly IUserDialogService _userDialogService;

        public LearnController(IDialogService dialogService, IUserDialogService userDialogService)
        {
            _dialogService = dialogService;
            _userDialogService = userDialogService;
        }

        // GET: Learn
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddDialogToCollection(int dialogId)
        {
            var userId = User.Identity.GetUserId();

            _userDialogService.AddUserDialog(dialogId, userId);

            return RedirectToAction("Overview");
        }


    }
}