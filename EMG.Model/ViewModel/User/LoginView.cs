using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EMG.Model
{
    public class LoginView
    {
        [DisplayName("帳號")]
        [Required(ErrorMessage = "請輸入帳號")]
        public string Account { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        public string Password { get; set; }
    }
}
