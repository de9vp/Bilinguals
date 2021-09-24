using Bilinguals.Domain.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Domain.Interfaces
{
    public interface IUserSentenceService
    {
        UserSentence AddOrUpdateUserSentence(int sentenceId, string userId);

        void Remove(int userSentenceId);

        IPagedList<Sentence> GetUserSentences(string userId, int pageIndex, int pageSize, string sortOrder);


    }
}
