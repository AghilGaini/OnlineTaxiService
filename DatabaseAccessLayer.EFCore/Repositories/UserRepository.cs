using CoreServices;
using DatabaseAccessLayer.EFCore.Contexts;
using DatabaseDomain.DTOs.Account.Register;
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

        public async Task<bool> IsDuplicateByUsernameAndUserType(string username, int userType, long id)
        {
            return await _context.Users.AnyAsync(r => r.Username == username && r.UserType == userType && r.Id != id);
        }

        public async Task<bool> RegisterUserDTO(RegisterDTO registerDTO)
        {
            if (registerDTO == null)
                return false;

            var newUser = new UserDomain()
            {
                IsActive = false,
                IsAdmin = false,
                Password = registerDTO.Password,
                Username = registerDTO.Username,
                UserType = registerDTO.UserType,
                CreatedOn = DateTime.Now
            };

            await _context.Users.AddAsync(newUser);

            return true;

        }
    }
}
