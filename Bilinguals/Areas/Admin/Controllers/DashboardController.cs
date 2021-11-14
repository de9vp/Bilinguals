using Bilinguals.Domain;
using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bilinguals.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ISentenceService _sentenceService;
        private readonly IDialogService _dialogService;
        private readonly IRepository<Sentence> _sentenceRepo;
        private readonly IRepository<Dialog> _dialogRepo;
        private readonly IRepository<ApplicationUser> _userRepo;

        public DashboardController(
            ISentenceService sentenceService,
            IDialogService dialogService,
            IRepository<Sentence> sentenceRepo,
            IRepository<Dialog> dialogRepo,
            IRepository<ApplicationUser> userRepo)
        {
            _sentenceService = sentenceService;
            _dialogService = dialogService;
            _sentenceRepo = sentenceRepo;
            _dialogRepo = dialogRepo;
            _userRepo = userRepo;
        }

        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            ViewBag.allSentences = _sentenceRepo.Table.Count();
            ViewBag.allDialogs = _dialogRepo.Table.Count();
            ViewBag.allUsers = _userRepo.Table.Count();
            return View();
        }
    }
}