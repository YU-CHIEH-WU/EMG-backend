using EMG.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EMG.Model
{
    public class MessageListView
    {
        [DisplayName("搜尋")]
        public string Search { get; set; }

        public ForPaging Paging { get; set; }
        public List<MessageView> MessageList { get; set; }

        public string Identity { get; set; }
        public string Role { get; set; }
    }
}