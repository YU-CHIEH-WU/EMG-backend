using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMG.Model
{
    public class ChangePasswordView
    {
        [DisplayName("舊密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密碼需介於6~20字元")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [DisplayName("新密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密碼需介於6~20字元")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DisplayName("密碼確認")]
        [Required(ErrorMessage = "請輸入密碼")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密碼需介於6~20字元")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "密碼與確認密碼不一致")]
        public string PasswordCheck { get; set; }
    }
}