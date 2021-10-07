using Bilinguals.Domain.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Domain.Interfaces
{
    public interface ISentenceService
    {
        void Add(Sentence sentence);

        void Delete(int id);

        void Edit(Sentence sentence);

        Sentence GetById(int id);

        IPagedList<Sentence> GetSentenceList(int pageIndex, int pageSize, string searchText, string sortOrder);

        IPagedList<Sentence> GetSentenceHome(int pageIndex, int pageSize, string searchText, string sortOrder, string userId);
    }
}
