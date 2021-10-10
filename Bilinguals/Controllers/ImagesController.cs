using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bilinguals.Data;
using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;

namespace Bilinguals.Controllers
{
    public class ImagesController : Controller
    {
        private readonly IImageService _imageService;

        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }

        // GET: Images/Create
        public ActionResult Create()
        {
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

                var extension = Path.GetExtension(ImageFile.FileName); // return .jpg  .png

                var fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName) + extension;

                var image = new Image
                {
                    ImagePath = "~/Avatars/" + fileName,
                };

                _imageService.Add(image);

                ImageFile.SaveAs(Path.Combine(Server.MapPath("~/Avatars/"), fileName)); // save to avatars folder

                return RedirectToAction("Index");
            }

            return View(ImageFile);
        }

        //// GET: Images/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Image image = db.Images.Find(id);
        //    if (image == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(image);
        //}

        //// POST: Images/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,ImagePath,DateCreated,DateModified")] Image image)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(image).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(image);
        //}

        //// GET: Images/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Image image = db.Images.Find(id);
        //    if (image == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(image);
        //}

        //// POST: Images/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Image image = db.Images.Find(id);
        //    db.Images.Remove(image);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
