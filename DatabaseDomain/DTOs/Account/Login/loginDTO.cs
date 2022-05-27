using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDomain.DTOs.Account.Login
{
    public class loginDTO
    {
        [Required(ErrorMessage = "نام کاربری اجباری میباشد")]
        public string Username { get; set; }
        [Required(ErrorMessage = "رمز عبور اجباری میباشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
