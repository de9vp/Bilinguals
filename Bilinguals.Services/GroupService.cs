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

        public GroupService(IRepository<Group> groupRepo)
        {
            _groupRepo = groupRepo;
        }

        public void Add(Group group)
        {
            _groupRepo.Insert(group);
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

        public IEnumerable<Group> GetByUserId(string UserId)
        {
            var groups = _groupRepo.Table.Where(x => x.UserId == UserId);
            return groups;
        }
    }
}
