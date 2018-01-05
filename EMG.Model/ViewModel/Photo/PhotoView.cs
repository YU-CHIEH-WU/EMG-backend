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
    public class PhotoView
    {
        [DisplayName("上傳圖片")]
        [FileExtensions(ErrorMessage = "所上傳檔案不是圖片")]
        public HttpPostedFileBase upload { get; set; }


        public List<Photo> PhotoList { get; set; }

        [DisplayName("帳號：")]
        public string Account { get; set; }

        [DisplayName("相片名稱：")]
        public string Img { get; set; }

        [DisplayName("相片路徑")]
        public string Url { get; set; }

        [DisplayName("新增時間：")]
        public string CreateTime { get; set; }
    }
}
