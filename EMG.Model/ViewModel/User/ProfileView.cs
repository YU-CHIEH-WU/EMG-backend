using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMG.Model
{
    public class ProfileView
    {
        [DisplayName("帳號：")]
        [Required(ErrorMessage = "帳號")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "帳號至少要輸入8字元")]
        //[Remote("CheckAccount", "Remote", ErrorMessage = "帳號已重複")]
        public string Account { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "密碼需介於6~20字元")]
        public string Password { get; set; }

        [DisplayName("姓名：")]
        [Required(ErrorMessage = "請輸入姓名")]
        public string Name { get; set; }

        [DisplayName("性別：")]
        [Required(ErrorMessage = "請輸入性別")]
        public string Sex { get; set; }

        [DisplayName("出生日期：")]
        [Required(ErrorMessage = "請輸入出生日期")]
        public DateTime Birth { get; set; }

        [DisplayName("年齡：")]
        public string Age { get; set; }

        [DisplayName("血型：")]
        public string Blood { get; set; }

        [DisplayName("身高(Cm)：")]
        public float Height { get; set; }

        [DisplayName("體重(Kg)：")]
        public float Weight { get; set; }

        [DisplayName("體脂：")]
        public float Bodyfat { get; set; }

        [DisplayName("基礎代謝率：")]
        public float BMR { get; set; }

        [DisplayName("病歷：")]
        public string Disease { get; set; }

        [DisplayName("E-mail：")]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("手機號碼：")]
        public string Phone { get; set; }

        [DisplayName("積分：")]
        public int Score { get; set; }
    }
}
