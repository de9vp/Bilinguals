using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Services
{
    public class GroupService : IGroupService
    {
        private readonly IRepository<Group> _groupRepo;
        private readonly IRepository<UserSentence> _userSentenceRepo;

        public GroupService(IRepository<Group> groupRepo, IRepository<UserSentence> userSentenceRepo)
        {
            _groupRepo = groupRepo;
            _userSentenceRepo = userSentenceRepo;
        }

        public void Add(Group group)
        {
            var g = _groupRepo.Table.FirstOrDefault(x => group.Name.Contains(x.Name)); //check name exist
            if (g == null)
            {
                _groupRepo.Insert(group);
            }
        }

        public void Delete(int id)
        {
            var group = _groupRepo.Table.FirstOrDefault(x => x.Id == id);
            _groupRepo.Delete(group);
        }

        public void Edit(Group group)
        {
            _groupRepo.Update(group);
        }

        public Group GetById(int id)
        {
            return _groupRepo.Table.FirstOrDefault(x => x.Id == id);
        }

        public IList<Group> GetByUserId(string UserId)
        {
            var groups = _groupRepo.Table.Where(x => x.UserId == UserId).ToList();

            var group = from g in groups
                        from us in _userSentenceRepo.Table.Where(x => x.UserId == UserId && x.GroupId == g.Id).DefaultIfEmpty()
                        select new Group
                        {
                            Id = g.Id,
                            Name = g.Name,
                            UserId = g.UserId,
                            Description = g.Description,
                            DateCreated = g.DateCreated,
                            DateModified = g.DateModified,
                            UserSentenceId = us == null ? (int?)null : us.Id,
                            SentenceId = us == null ? (int?)null : us.SentenceId,
                        };
            return group.ToList();
        }
    }
}
