using DatabaseDomain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDomain.DTOs.Security.Role
{
    public class RoleDTO : ActionItem
    {

        public RoleDTO()
        {
            Actions = new List<ActionItem>();
            RoleInfos = new List<RoleInfoDTO>();
        }

        public List<ActionItem> Actions { get; set; }
        public List<RoleInfoDTO> RoleInfos { get; set; }
    }

    public class RoleInfoDTO
    {
        [Display(Name = "شناسه")]
        public long Id { get; set; }
        [Required]
        [Display(Name = "عنوان")]
        public string Title { get; set; }
    }
}
