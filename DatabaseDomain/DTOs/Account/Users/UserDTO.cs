using DatabaseDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDomain.DTOs.Account.Users
{
    public class UserDTO
    {
        public UserDTO()
        {
            Users = new List<UserInfoDTO>();
            Actions = new List<ActionItem>();
        }
        public List<UserInfoDTO> Users { get; set; }
        public List<ActionItem> Actions { get; set; }
    }

    public class UserInfoDTO
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public int UserType { get; set; }
        public string UserTypeTitle { get; set; }
        public bool IsActive { get; set; }

    }
}
