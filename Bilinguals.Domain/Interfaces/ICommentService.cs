using Bilinguals.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Domain.Interfaces
{
    public interface ICommentService
    {
        Comment Add(Comment comment);
        List<Comment> GetByDialogId(int dialogId);
        void Delete(int commentId);
        Comment Edit(Comment comment);
        Comment GetById(int id);
    }
}
