using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDomain.DTOs.Security.Permision
{
    public class PermisionDTO
    {
        public PermisionDTO()
        {
            Permisions = new List<PermisionInfoDTO>();
        }
        public long RoleId { get; set; }
        public List<PermisionInfoDTO> Permisions { get; set; }
    }
    public class PermisionInfoDTO
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public string Title { get; set; }
        public bool IsSelected { get; set; }
    }
}
