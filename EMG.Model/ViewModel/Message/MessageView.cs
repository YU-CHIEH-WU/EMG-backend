using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EMG.Model
{
    public class MessageView
    {
        [DisplayName("編號：")]
        public int MId { get; set; }

        [DisplayName("標題：")]
        [Required(ErrorMessage = "請輸入標題")]
        [StringLength(50, ErrorMessage = "標題長度最多50字元")]
        public string Title { get; set; }

        [DisplayName("留言內容：")]
        [Required(ErrorMessage = "請輸入留言內容")]
        public string Messages { get; set; }

        [DisplayName("留言帳號：")]
        [Required(ErrorMessage = "請輸入帳號")]
        public string Account { get; set; }

        [DisplayName("留言者：")]
        public string UserName { get; set; }

        [DisplayName("大頭貼：")]
        public string Photo { get; set; }

        [DisplayName("新增時間：")]
        public DateTime CreateTime { get; set; }

        public bool IsEdit { get; set; }
        public string Identity { get; set; }
        public string Role { get; set; }
    }
}