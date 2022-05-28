using CoreServices;
using DatabaseDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDomain.Interfaces
{
    public interface IUserDomain : IGenericDomain<UserDomain>
    {
        Task<UserDomain> GetByUsername(string username);
        Task<UserDomain> GetByUsernameAndUserType(string username, int userType);
    }
}
