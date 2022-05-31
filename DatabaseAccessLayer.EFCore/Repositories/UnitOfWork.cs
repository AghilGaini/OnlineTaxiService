using DatabaseAccessLayer.EFCore.Contexts;
using DatabaseDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLayer.EFCore.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public IRoleDomain _role { get; set; }
        public IUserDomain _user { get; set; }
        public IPermisionDomain _permision { get; set; }
        public IRolePermisionDomain _rolePermision { get; set; }
        public IUserRoleDomain _userRole { get; set; }

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            _role = new RoleRepository(context);
            _user = new UserRepository(context);
            _permision = new PermisionRepository(context);
            _rolePermision = new RolePermisionRepository(context);
            _userRole = new UserRoleRepository(context);
        }

        public void Commit()
        {
            _context.SaveChanges();
            Dispose();
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }
}
