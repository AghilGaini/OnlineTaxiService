using DatabaseDomain.DTOs.Security.Role;
using DatabaseDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDomain.Interfaces
{
    public interface IRoleDomain : IGenericDomain<RoleDomain>
    {
        Task<RoleDTO> GetRolesAsync();
        Task<bool> IsDuplicateByName(long id, string name);
        Task AddRoleAcyncDTO(NewRoleDTO roleDTO);

        Task<bool> UpdateRoleDTO(UpdateRoleDTO roleDTO);
    }
}
