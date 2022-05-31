using DatabaseAccessLayer.EFCore.Contexts;
using DatabaseDomain.DTOs.Account.UserRoles;
using DatabaseDomain.Entities;
using DatabaseDomain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLayer.EFCore.Repositories
{
    public class UserRoleRepository : GenericRepository<UserRoleDomain>, IUserRoleDomain
    {
        private readonly ApplicationContext _context;

        public UserRoleRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AddRangeUserRoleDTO(UserRolesDTO userRolesInfoDTO)
        {
            var userId = userRolesInfoDTO.UserId;

            await _context.UserRoles.AddRangeAsync(userRolesInfoDTO.Roles.Select(r => new UserRoleDomain()
            {
                RoleId = r.RoleId,
                UserId = userId,
            }).ToList());

            return true;

        }

        public async Task<bool> DeleteByUserId(long userId)
        {
            var userRoles = await _context.UserRoles.Where(r => r.UserId == userId).ToListAsync();

            _context.UserRoles.RemoveRange(userRoles);

            return true;
        }

        public async Task<List<long>> GetRolesByUserId(long userId)
        {
            return await _context.UserRoles.Where(x => x.UserId == userId).Select(r => r.RoleId).ToListAsync();
        }
    }
}
