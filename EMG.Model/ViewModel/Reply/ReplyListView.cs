using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMG.Model
{
    public class ReplyListView
    {
        [DisplayName("搜尋")]
        public string Search { get; set; }

        [DisplayName("回覆編號：")]
        public int rID { get; set; }

        public int mId { get; set; }

        public ForPaging Paging { get; set; }
        public List<Reply> DataList { get; set; }

        [DisplayName("新增時間：")]
        public DateTime ReplyTime { get; set; }

        public string Identity { get; set; }
        public string Role { get; set; }
    }
}
