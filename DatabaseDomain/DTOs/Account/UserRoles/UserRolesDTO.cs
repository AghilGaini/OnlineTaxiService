using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDomain.DTOs.Account.UserRoles
{
    public class UserRolesDTO
    {
        public UserRolesDTO()
        {
            Roles = new List<UserRolesInfoDTO>();
        }
        public long UserId { get; set; }
        public List<UserRolesInfoDTO> Roles { get; set; }
    }
    public class UserRolesInfoDTO
    {
        public long RoleId { get; set; }
        public string RoleTitle { get; set; }
        public bool IsSelected { get; set; }
    }
}
