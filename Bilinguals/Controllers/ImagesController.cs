using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bilinguals.App;
using Bilinguals.Data;
using Bilinguals.Domain;
using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;
using Bilinguals.Services;
using Microsoft.AspNet.Identity;

namespace Bilinguals.Controllers
{
    public class ImagesController : Controller
    {
        private readonly IImageService _imageService;
        private readonly IUserService _userService;

        public ImagesController(IImageService imageService, IUserService userService)
        {
            _imageService = imageService;
            _userService = userService;
        }

        // GET: Images/Create
        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
                return PartialView(); //ajax request
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile == null)
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }

                var fileType = Path.GetExtension(ImageFile.FileName); // return .jpg  .png

                var fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName) + User.Identity.GetUserId();

                var filePath = fileName + fileType;

                var image = new Image
                {
                    ImagePath = filePath,
                };

                _imageService.Add(image);

                ImageFile.SaveAs(Path.Combine(Server.MapPath("~/Avatars/"), filePath)); // save to avatars folder

                // update user
                var newImage = _imageService.FindByPath(image.ImagePath);
                _userService.UpdateImage(User.Identity.GetUserId(), newImage.Id);

                if (Request.IsAjaxRequest())
                    return Json(JsonResultHelper.MapImageJson(newImage), JsonRequestBehavior.AllowGet);

                return RedirectToAction("Index");
            }

            return View(ImageFile);
        }

        // GET: Images/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var image = _imageService.FindById(id.Value);
            if (image == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
                return PartialView(id); //ajax request
            return View(image);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase ImageFile, int? id)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile == null)
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }

                // Delete file from folder
                var oldImage = _imageService.FindById(id.Value);
                FileInfo oldFile = new FileInfo( Server.MapPath("~/Avatars/" + oldImage.ImagePath) );
                oldFile.Delete();

                var fileType = Path.GetExtension(ImageFile.FileName); // return .jpg  .png

                var fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName) + User.Identity.GetUserId();

                var filePath = fileName + fileType;

                var image =_imageService.Edit(id.Value, filePath);

                ImageFile.SaveAs(Path.Combine(Server.MapPath("~/Avatars/"), filePath)); // save new file to avatars folder

                if (Request.IsAjaxRequest())
                    return Json(JsonResultHelper.MapImageJson(image), JsonRequestBehavior.AllowGet);

                return RedirectToAction("Index");
            }
            return View(ImageFile);
        }

        // GET: Images/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var image = _imageService.FindById(id.Value);
            if (image == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
                return PartialView(id); //ajax request
            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id, string returnUrl = null)
        {
            _userService.DeleteImage(User.Identity.GetUserId());

            // Delete file from folder
            var image = _imageService.FindById(id.Value);
            FileInfo oldFile = new FileInfo(Server.MapPath("~/Avatars/" + image.ImagePath));
            oldFile.Delete();

            _imageService.Delete(id.Value);

            if (Request.IsAjaxRequest())
                return Json("", JsonRequestBehavior.AllowGet);

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Manage");
        }
    }
}
