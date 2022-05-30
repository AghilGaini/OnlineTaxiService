using DatabaseDomain.DTOs.Security.Permision;
using DatabaseDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDomain.Interfaces
{
    public interface IPermisionDomain : IGenericDomain<PermisionDomain>
    {
        Task<bool> AddRange(List<PermisionDomain> permisions);
        Task<PermisionDTO> GetAllPermisionsDTO();
    }
}
