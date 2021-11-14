using Bilinguals.Domain.Interfaces;
using Bilinguals.Domain.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilinguals.Services
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole, string>
    {
        private readonly IRepository<ApplicationUserRole> _userRoleRepo;
        private readonly IRepository<ApplicationRole> _roleRepo;
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store, IRepository<ApplicationUserRole> userRoleRepo, IRepository<ApplicationRole> roleRepo)
            : base(store)
        {
            _userRoleRepo = userRoleRepo;
            _roleRepo = roleRepo;
        }

        public void AddRole(string userId, string roleId)
        {
            _userRoleRepo.Insert(new ApplicationUserRole { UserId = userId, RoleId = roleId });
        }

        public void UpdateRole(ApplicationUserRole userRole)
        {
            _userRoleRepo.Update(userRole);
        }

        public List<ApplicationRole> GetAll()
        {
            return _roleRepo.Table.ToList();
        }
    }
}
