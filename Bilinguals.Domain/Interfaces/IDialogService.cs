using Bilinguals.Domain.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Domain.Interfaces
{
    public interface IDialogService
    {
        void Add(Dialog dialog);

        void Delete(int id);

        void Edit(Dialog dialog);

        Dialog GetById(int id);

        List<Dialog> GetAll();

        IPagedList<Dialog> GetDialogList(int pageIndex, int pageSize, string searchText, string sortOrder, string userId);

        void FromTextFile(string allTexts);

        Dialog GetDialogDetailAndSentences(int dialogId, string userId);
    }
}
