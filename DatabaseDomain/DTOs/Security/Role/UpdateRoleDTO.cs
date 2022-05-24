using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDomain.DTOs.Security.Role
{
    public class UpdateRoleDTO
    {
        [Required(ErrorMessage = "شناسه الزامی میباشد")]
        public long Id { get; set; }
        [Required(ErrorMessage = "عنوان الزامی میباشد ")]
        public string Title { get; set; }
    }
}
