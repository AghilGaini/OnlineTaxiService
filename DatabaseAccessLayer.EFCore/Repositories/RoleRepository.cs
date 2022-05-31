using DatabaseAccessLayer.EFCore.Contexts;
using DatabaseDomain.DTOs.Account.UserRoles;
using DatabaseDomain.DTOs.Security.Role;
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
    public class RoleRepository : GenericRepository<RoleDomain>, IRoleDomain
    {
        private readonly ApplicationContext _context;

        public RoleRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddRoleAcyncDTO(NewRoleDTO roleDTO)
        {
            var newRole = new RoleDomain()
            {
                Title = roleDTO.Title
            };

            await _context.Roles.AddAsync(newRole);
        }

        public async Task<RoleDTO> GetRolesAsync()
        {
            var res = new RoleDTO();
            res.RoleInfos.AddRange(await _context.Roles.Select(r => new RoleInfoDTO() { Id = r.Id, Title = r.Title }).ToListAsync());
            return res;
        }
        public Task<bool> IsDuplicateByName(long id, string name)
        {
            return _context.Roles.Where(r => r.Id != id && r.Title == name).AnyAsync();
        }

        public async Task<bool> UpdateRoleDTO(UpdateRoleDTO roleDTO)
        {
            var oldRole = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleDTO.Id);
            if (oldRole == null)
                return false;
            oldRole.Title = roleDTO.Title;
            return true;
        }

        async Task<UserRolesDTO> IRoleDomain.GetRolesDTO()
        {
            var userRoleDTO = new UserRolesDTO();

            userRoleDTO.Roles.AddRange(await _context.Roles.Select(r => new UserRolesInfoDTO()
            {
                RoleId = r.Id,
                RoleTitle = r.Title,
                IsSelected = false
            }
           ).ToListAsync());

            return userRoleDTO;
        }
    }
}
