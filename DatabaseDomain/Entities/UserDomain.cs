using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDomain.Entities
{
    public class UserDomain
    {
        [Required(ErrorMessage = "شناسه اجباری میباشد")]
        public long Id { get; set; }
        [Required(ErrorMessage = "نام کاربری اجباری میباشد")]
        public string Username { get; set; }
        [Required(ErrorMessage = "رمز عبور اجباری میباشد")]
        public int UserType { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsAdmin { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifyOn { get; set; }
        public string ModifyBy { get; set; }
    }
}
