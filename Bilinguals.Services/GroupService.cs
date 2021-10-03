using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            return groups;
        }
    }
}
