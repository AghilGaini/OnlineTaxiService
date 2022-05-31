using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDomain.DTOs.Account.Roles
{
    public class RoleDTO
    {
        public long RoleId { get; set; }
        public string RoleTitle { get; set; }
        public bool IsSelected { get; set; }
    }
}
