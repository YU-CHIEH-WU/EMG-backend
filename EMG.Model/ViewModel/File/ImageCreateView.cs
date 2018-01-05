using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMG.Model
{
    public class ImageCreateView
    {
        [DisplayName("編號")]
        public int PId { get; set; }

        [DisplayName("圖片名稱")]
        public string Img { get; set; }

        [DisplayName("圖片路徑")]
        public string Url { get; set; }
    }
}
