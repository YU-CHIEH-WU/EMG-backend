using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMG.Model.ViewModel
{
    public class RMSView
    {
        [DisplayName("編號：")]
        public int R_Id { get; set; }

        [DisplayName("帳號：")]
        [Required(ErrorMessage = "請輸入帳號")]
        public string Account { get; set; }

        [DisplayName("第幾次訓練：")]
        public int Training { get; set; }

        [DisplayName("第幾組：")]
        public int Series { get; set; }

        [DisplayName("次數：")]
        public int Times { get; set; }

        [DisplayName("RMS：")]
        public string RMS { get; set; }

        [DisplayName("%MVC：")]
        public int PMVC { get; set; }

        [DisplayName("新增時間：")]
        public DateTime CreateTime { get; set; }
    }
}
