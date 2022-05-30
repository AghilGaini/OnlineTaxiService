using DatabaseAccessLayer.EFCore.Contexts;
using DatabaseDomain.DTOs.Security.Permision;
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
    public class PermisionRepository : GenericRepository<PermisionDomain>, IPermisionDomain
    {
        private readonly ApplicationContext _context;

        public PermisionRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AddRange(List<PermisionDomain> permisions)
        {
            await _context.Permisions.AddRangeAsync(permisions);

            return true;
        }

        public async Task<PermisionDTO> GetAllPermisionsDTO()
        {
            var permisionDTO = new PermisionDTO();

            permisionDTO.Permisions = await _context.Permisions.Select(r =>
               new PermisionInfoDTO()
               {
                   Title = r.Title,
                   Value = r.Value,
                   IsSelected = false,
                   Id = r.Id
               }).ToListAsync();

            return permisionDTO;
        }
    }
}
