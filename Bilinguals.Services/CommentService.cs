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

        public void Add(Comment comment)
        {
            _commentRepo.Insert(comment);
        }
    }
}
