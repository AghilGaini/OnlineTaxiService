using CoreServices;
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
    public class UserRepository : GenericRepository<UserDomain>, IUserDomain
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserDomain> GetByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(r => r.Username == username);
        }

        public async Task<UserDomain> GetByUsernameAndUserType(string username, int userType)
        {
            return await _context.Users.FirstOrDefaultAsync(r => r.Username == username && r.UserType == userType);
        }
    }
}
