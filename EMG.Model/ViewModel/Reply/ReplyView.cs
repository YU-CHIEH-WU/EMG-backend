using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMG.Model
{
    public class ReplyView
    {
        [DisplayName("回覆編號：")]
        public int rID { get; set; }

        [DisplayName("留言編號：")]
        public int mId { get; set; }

        [DisplayName("回覆內容：")]
        [Required(ErrorMessage = "請輸入留言內容")]
        public string Replys { get; set; }

        [DisplayName("留言帳號：")]
        [Required(ErrorMessage = "請輸入帳號")]
        public string Account { get; set; }

        [DisplayName("留言者：")]
        public string UserName { get; set; }

        public string Photo { get; set; }

        [DisplayName("新增時間：")]
        public DateTime ReplyTime { get; set; }

        public string Identity { get; set; }
        public string Role { get; set; }
    }
}
