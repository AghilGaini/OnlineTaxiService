using DatabaseAccessLayer.EFCore.Contexts;
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

        public async Task<List<long>> GetPermisionsIdByRoleId(long roleId)
        {
            return await _context.RolePermisions.Where(r => r.RoleId == roleId).Select(r => r.PermisionId).ToListAsync();
        }
    }
}
