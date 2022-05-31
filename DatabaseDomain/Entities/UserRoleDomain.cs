using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDomain.Entities
{
    public class UserRoleDomain
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long RoleId { get; set; }
    }
}
