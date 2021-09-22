using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Services
{
    public class UserDialogService : IUserDialogService
    {
        private readonly IRepository<UserDialog> _userDialogRepo;
        private readonly IDialogService _dialogService;

        public UserDialogService(IRepository<UserDialog> userDialogRepo, IDialogService dialogService)
        {
            _userDialogRepo = userDialogRepo;
            _dialogService = dialogService;
        }

        public UserDialog AddUserDialog(int dialogId, string userId)
        {
            var dialog = _dialogService.GetById(dialogId);

            if (dialog == null)
            {
                throw new ArgumentNullException("Dialog not found");
            }

            //check if this UserDialog already axists
            var userDialog = _userDialogRepo.Table.FirstOrDefault(x => x.DialogId == dialogId && x.UserId == userId);
            if (userDialog != null)
            {
                return userDialog;
            }

            userDialog = new UserDialog
            {
                DialogId = dialog.Id,
                UserId = userId,
                IsLearning = true
            };

            _userDialogRepo.Insert(userDialog);

            return userDialog;
        }

        public void Delete(int userDialogId)
        {
            var userDialog = _userDialogRepo.Table.FirstOrDefault(x => x.Id == userDialogId);
            _userDialogRepo.Delete(userDialog);
        }
    }
}
