using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMG.Model
{
    public class FileView
    {
        [DisplayName("檔案編號")]
        public int Id { get; set; }

        [DisplayName("版本編號")]
        public int Edition { get; set; }

        [DisplayName("檔案名稱")]
        public string Name { get; set; }

        public string Url { get; set; }

        [DisplayName("檔案類型")]
        public string Type { get; set; }

        public int Size { get; set; }

        public string Account { get; set; }

        [DisplayName("新增人")]
        public string UserName { get; set; }

        public bool IsTrash { get; set; }

        [DisplayName("新增時間")]
        public DateTime Date { get; set; }

        [DisplayName("註解")]
        public string Remark { get; set; }
    }
}
