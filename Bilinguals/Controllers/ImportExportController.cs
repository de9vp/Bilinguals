using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace Bilinguals.Controllers
{
    [Authorize(Roles = "Administrator, Creater")]
    public class ImportExportController : Controller
    {
        private readonly IDialogService _dialogService;
        private readonly ISentenceService _sentenceService;

        public ImportExportController(IDialogService dialogService, ISentenceService sentenceService)
        {
            _dialogService = dialogService;
            _sentenceService = sentenceService;
        }

        // GET: ImportExport
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Post of data for language import (file info)
        /// </summary>
        /// <param name="languageCulture">This defines the name etc of the imported language</param>
        /// <param name="file">The name-value pairs for the language content</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            using (var scope = new TransactionScope())
            {
                ViewBag.Result = "success";
                // http://www.dustinhorne.com/post/2011/11/16/AJAX-File-Uploads-with-jQuery-and-MVC-3.aspx
                try
                {
                    // Verify that the user selected a file
                    if (file != null && file.ContentLength > 0)
                    {
                        // Unpack the data
                        var allTexts = string.Empty;
                        using (var streamReader = new StreamReader(file.InputStream, System.Text.Encoding.UTF8, true))
                        {
                            while (streamReader.Peek() >= 0)
                            {
                                allTexts = streamReader.ReadToEnd();
                            }
                        }

                        _dialogService.FromTextFile(allTexts);

                        // Read the CSV file and generate a language
                        //report = _localizationService.FromCsv(languageCulture, allLines);
                        scope.Complete();
                    }
                    else
                    {
                        throw new Exception("File does not contain data");
                    }
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    ViewBag.Result = ex.Message;
                }

                return View();
            }
        }

        public ActionResult DialogSend(string inputStr)
        {
            var inputArr = inputStr.Split('|');
            var dialog = new Dialog
            {
                Name = inputArr[1],
                Description = inputArr[2]
            };
            _dialogService.Add(dialog);

            var newDialog = _dialogService.GetByName(dialog.Name);
            var sortOrder = 1;
            for (int i = 3; i < inputArr.Length; i = i + 2)
            {
                var sentence = new Sentence
                {
                    EnText = inputArr[i],
                    ViText = inputArr[i+1],
                    DialogId = newDialog.Id,
                    SortOrder = sortOrder,
                };
                _sentenceService.Add(sentence);
                sortOrder++;
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}

