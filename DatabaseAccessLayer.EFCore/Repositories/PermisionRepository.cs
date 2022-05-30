using DatabaseAccessLayer.EFCore.Contexts;
using DatabaseDomain.Entities;
using DatabaseDomain.Interfaces;
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
    }
}
