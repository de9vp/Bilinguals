using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _commentRepo;

        public CommentService(IRepository<Comment> commentRepo)
        {
            _commentRepo = commentRepo;
        }

        public Comment Add(Comment comment)
        {
            _commentRepo.Insert(comment);
            return _commentRepo.Table.FirstOrDefault(x => x.Text == comment.Text);
        }

        public List<Comment> GetByDialogId(int dialogId)
        {
            var comments = _commentRepo.Table.Where(x => x.DialogId == dialogId).OrderBy(x => x.TimeStamp).ToList();
            return comments;
        }
    }
}
