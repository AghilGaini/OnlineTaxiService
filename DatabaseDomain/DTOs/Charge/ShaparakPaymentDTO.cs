using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDomain.DTOs.Charge
{
    public class ShaparakPaymentDTO
    {
        [Required(ErrorMessage = "شماره کارت اجباری است")]
        public string Cardno { get; set; }
        [Required(ErrorMessage = "CVV2 اجباری است")]
        public long CVV2 { get; set; }
        [Required(ErrorMessage = "سال تاریخ انقضا اجباری است")]
        public string ExpireYear { get; set; }
        [Required(ErrorMessage = "ماه تاریخ انقضا اجباری است")]
        public string ExpireMonth { get; set; }
        [Required(ErrorMessage = "رمز کارت اجباری است")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
        public long Amount { get; set; }

    }
}
