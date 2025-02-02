﻿using Bilinguals.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Domain.Interfaces
{
    public interface IGroupService
    {
        Group Add(Group group, string userId);

        void Delete(int id);

        void Edit(Group group);

        Group GetById(int id);

        IList<Group> GetByUserId(string userId);
    }
}
