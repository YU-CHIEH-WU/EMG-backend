using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMG.Model.ViewModel.News
{
    public class NewsView
    {
        [DisplayName("編號：")]
        public int N_Id { get; set; }

        [DisplayName("帳號：")]
        [Required(ErrorMessage = "請輸入帳號")]
        public string Account { get; set; }

        [DisplayName("標題：")]
        [Required(ErrorMessage = "請輸入標題")]
        [StringLength(50, ErrorMessage = "標題長度最多50字元")]
        public string Title { get; set; }

        [DisplayName("內容：")]
        [Required(ErrorMessage = "請輸入留言內容")]
        public string Detail { get; set; }

        [DisplayName("新增時間：")]
        public DateTime CreateTime { get; set; }
    }
}
