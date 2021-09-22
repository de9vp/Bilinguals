using Bilinguals.Domain.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Domain.Interfaces
{
    public interface IUserDialogService
    {
        UserDialog AddUserDialog(int dialogId, string userId);

        void Delete(int userDialogId);
        IPagedList<Dialog> GetUserDialogs(string userId, int pageIndex, int pageSize, string sortOrder);
    }
}
