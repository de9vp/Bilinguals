using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;
using PagedList;
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
        private readonly IRepository<Dialog> _dialogRepo;
        private readonly IDialogService _dialogService;

        public UserDialogService(IRepository<UserDialog> userDialogRepo, IRepository<Dialog> dialogRepo, IDialogService dialogService)
        {
            _userDialogRepo = userDialogRepo;
            _dialogRepo = dialogRepo;
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

            if (userDialog != null)
            {
                _userDialogRepo.Delete(userDialog);
            }
        }

        public IPagedList<Dialog> GetUserDialogs(string userId, int pageIndex, int pageSize, string sortOrder)
        {
            var dialogIds = _userDialogRepo.Table
                                .Where(x => x.UserId == userId)
                                .Select(x => x.DialogId)
                                .ToList();
            var userDialogs = _dialogRepo.Table.Where(x => dialogIds.Contains(x.Id));

            switch (sortOrder)
            {
                //case "": 
                //    userDialogs = userDialogs.OrderByDescending(x => x.DateCreated);
                //    break;
                default:
                    userDialogs = userDialogs.OrderBy(x => x.Id);
                    break;
            }

            return userDialogs.ToPagedList(pageIndex, pageSize);
        }
    }
}
