using DatabaseAccessLayer.EFCore.Contexts;
using DatabaseDomain.DTOs.Security.RolePermision;
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
    public class RolePermisionRepository : GenericRepository<RolePermisionDomain>, IRolePermisionDomain
    {
        private readonly ApplicationContext _context;

        public RolePermisionRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AddRolePermisionsDTO(List<RolePermisionDTO> rolePermisionDTOs)
        {
            await _context.RolePermisions.AddRangeAsync(
                rolePermisionDTOs.Select(r => new RolePermisionDomain()
                {
                    RoleId = r.RoleId,
                    PermisionId = r.PermisionId
                }).ToList()
                );

            return true;
        }

        public async Task<bool> DeleteByRoleId(long roleId)
        {
            var rolePermisions = await _context.RolePermisions.Where(r => r.RoleId == roleId).ToListAsync();

            _context.RolePermisions.RemoveRange(rolePermisions);

            return true;
        }

        public async Task<List<long>> GetPermisionsIdByRoleId(long roleId)
        {
            return await _context.RolePermisions.Where(r => r.RoleId == roleId).Select(r => r.PermisionId).ToListAsync();
        }
    }
}
