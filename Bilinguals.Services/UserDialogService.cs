using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            //var dialog = _dialogService.GetById(dialogId);

            //if (dialog == null)
            //{
            //    throw new ArgumentNullException("Dialog not found");
            //}

            //check if this UserDialog already axists
            var userDialog = _userDialogRepo.Table.FirstOrDefault(x => x.DialogId == dialogId && x.UserId == userId);
            if (userDialog != null)
            {
                //update
                userDialog.Learned = false;
                _userDialogRepo.Update(userDialog);
            }
            else
            {
                //add
                userDialog = new UserDialog
                {
                    DialogId = dialogId,
                    UserId = userId,
                    Learned = false
                };
                _userDialogRepo.Insert(userDialog);

            }

            return userDialog;
        }

        public void MarkLearned (int userDialogId)
        {
            var userDialog = _userDialogRepo.Table.FirstOrDefault(x => x.Id == userDialogId);

            if (userDialog != null)
            {
                userDialog.Learned = true;
                userDialog.DateLearned = DateTime.Now;
                _userDialogRepo.Update(userDialog);
            }
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
            var userDialogs = _userDialogRepo.Table
                                            .Where(x => x.UserId == userId)
                                            .Include(icl => icl.Dialog)
                                            .AsEnumerable() //luu y
                                            .Select(x => new Dialog
                                            {
                                                Id = x.DialogId,
                                                Name = x.Dialog.Name,
                                                Description = x.Dialog.Description,
                                                DateCreated = x.Dialog.DateCreated,
                                                DateModified = x.Dialog.DateModified,
                                                UserDialogId = x.Id
                                            }).ToPagedList(pageIndex, pageSize);

            return userDialogs;
        }
    }
}
