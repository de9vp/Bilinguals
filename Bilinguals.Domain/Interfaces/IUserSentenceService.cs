﻿using Bilinguals.Domain.Models;
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
        UserSentence AddOrUpdateUserSentence(int sentenceId, int groupId, string userId);

        void Remove(int userSentenceId);

    }
}
