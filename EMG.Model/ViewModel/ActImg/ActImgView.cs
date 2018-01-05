using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EMG.Model
{
    public class ActImgView
    {
        [DisplayName("封面圖片：")]
        [FileExtensions(ErrorMessage = "所上傳檔案不是圖片")]
        public HttpPostedFileBase upload { get; set; }

        public List<ActImg> actImgList { get;  set; }

        [DisplayName("分類名稱：")]
        public string Sort { get; set; }

        public string account { get; set; }

        [DisplayName("相簿描述：")]
        public string Brief { get; set; }       
    }
}
