using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDomain.DTOs.Account.Register
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "نام کاربری الزامی میباشد")]
        public string Username { get; set; }
        [Required(ErrorMessage = "رمز عبور الزامی میباشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "ایمیل الزامی میباشد")]
        public string Email { get; set; }
        public bool RememberMe { get; set; }
        public int UserType { get; set; }
    }
}
