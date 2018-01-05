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
    public class ImageVIew
    {
        [DisplayName("上傳圖片")]
        [FileExtensions(ErrorMessage = "所上傳檔案不是圖片")]
        public HttpPostedFileBase upload { get; set; }

        public ActImg actimg { get; set; }

        public List<Image> FileList { get; set; }

        [DisplayName("相簿：")]
        public string Sort { get; set; }

        [DisplayName("相簿描述：")]
        public string Brief { get; set; }

        [DisplayName("新增時間：")]
        public string CreateTime { get; set; }
    }
}
