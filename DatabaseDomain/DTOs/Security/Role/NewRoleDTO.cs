using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDomain.DTOs.Security.Role
{
    public class NewRoleDTO
    {
        [Required(ErrorMessage = "عنوان اجباری میباشد")]
        public string Title { get; set; }
    }
}
