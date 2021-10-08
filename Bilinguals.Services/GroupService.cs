using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

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

        public Group Add(Group group, string userId)
        {
            var g = _groupRepo.Table.FirstOrDefault(x => x.Name == group.Name && x.UserId == userId); //check name exist
            if (g == null)
            {
                g = new Group
                {
                    Name = group.Name,
                    UserId = userId,
                    Description = group.Description,
                };
                _groupRepo.Insert(g);
                var ng = _groupRepo.Table.FirstOrDefault(x => g.Name.Contains(x.Name) && x.UserId == g.UserId);
                return ng;
            }
            return g;
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
            var groups = _groupRepo.Table.Where(x => x.UserId == UserId).Include(icl => icl.UserSentences).ToList();
            return groups;
        }
    }
}
