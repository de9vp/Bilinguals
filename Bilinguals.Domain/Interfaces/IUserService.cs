using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Domain.Interfaces
{
    public interface IUserService
    {
        void UpdateImage(string userId, int imageId);
        void DeleteImage(string userId);
        ApplicationUser GetById(string userId);
    }
}
