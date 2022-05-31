using DatabaseDomain.DTOs.Account.UserRoles;
using DatabaseDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDomain.Interfaces
{
    public interface IUserRoleDomain : IGenericDomain<UserRoleDomain>
    {
        Task<List<long>> GetRolesByUserId(long userId);
        Task<bool> DeleteByUserId(long userId);
        Task<bool> AddRangeUserRoleDTO(UserRolesDTO userRolesInfoDTO);
    }
}
