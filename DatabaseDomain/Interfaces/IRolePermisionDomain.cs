using DatabaseDomain.DTOs.Security.RolePermision;
using DatabaseDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDomain.Interfaces
{
    public interface IRolePermisionDomain : IGenericDomain<RolePermisionDomain>
    {
        Task<List<long>> GetPermisionsIdByRoleId(long roleId);
        Task<bool> DeleteByRoleId(long roleId);
        Task<bool> AddRolePermisionsDTO(List<RolePermisionDTO> rolePermisionDTOs);
    }
}
