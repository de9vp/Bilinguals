using Bilinguals.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Domain.Interfaces
{
    public interface IUserSentenceService
    {
        UserSentence AddOrUpdateUserSentence(int sentenceId, string userId, bool featured);
        void Remove(int userSentenceId);
    }
}
