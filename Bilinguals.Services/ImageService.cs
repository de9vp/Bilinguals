using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Services
{
    public class ImageService : IImageService
    {
        private readonly IRepository<Image> _imageRepo;

        public ImageService(IRepository<Image> imageRepo)
        {
            _imageRepo = imageRepo;
        }

        public void Add(Image image)
        {
            _imageRepo.Insert(image);
        }

        public void Delete(int id)
        {
            var image = _imageRepo.Table.FirstOrDefault(x => x.Id == id);
            _imageRepo.Delete(image);
        }

        public void Edit(Image image)
        {
            _imageRepo.Update(image);
        }

        public Image FindByPath(string imagePath)
        {
            var image = _imageRepo.Table.FirstOrDefault(x => x.ImagePath == imagePath);
            return image;
        }

        public Image FindById(int id)
        {
            var image = _imageRepo.GetById(id);
            return image;
        }
    }
}
