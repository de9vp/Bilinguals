using Bilinguals.Domain;
using Bilinguals.Domain.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Services
{
    public class UserService : IUserService
    {
        private ApplicationUserManager _userManager;

        public UserService(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager;
            }
        }

        public void UpdateImage(string userId, int imageId)
        {
            var user = UserManager.FindById(userId);


            user.Id = user.Id;
            user.Email = user.Email;
            user.PasswordHash = user.PasswordHash;
            user.PhoneNumber = user.PhoneNumber;
            user.UserName = user.UserName;
            user.FirstName = user.FirstName;
            user.LastName = user.LastName;
            user.DateOfBirth = user.DateOfBirth;
            user.ImageId = imageId;
            

            UserManager.Update(user);
        }
    }
}
