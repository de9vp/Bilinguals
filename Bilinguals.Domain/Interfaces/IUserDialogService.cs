﻿using Bilinguals.Domain.Models;
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
    }
}
