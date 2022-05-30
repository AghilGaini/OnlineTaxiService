using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDomain.Entities
{
    public class RolePermisionDomain
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public long PermisionId { get; set; }
    }
}
