using Bilinguals.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Domain.Interfaces
{
    public interface IImageService
    {
        void Add(Image image);

        void Delete(int id);

        Image Edit(int imageId, string path);

        Image FindByPath(string imagePath);

        Image FindById(int id);
    }
}
