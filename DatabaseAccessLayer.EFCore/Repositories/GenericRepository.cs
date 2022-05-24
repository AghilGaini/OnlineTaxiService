using DatabaseAccessLayer.EFCore.Contexts;
using DatabaseDomain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLayer.EFCore.Repositories
{
    public abstract class GenericRepository<T> : IGenericDomain<T> where T : class
    {
        private readonly ApplicationContext _context;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(long id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> RemoveById(long id)
        {
            var t = await _context.Set<T>().FindAsync(id);
            if (t != null)
            {
                _context.Set<T>().Remove(t);
                return true;
            }
            return false;
        }
    }
}
