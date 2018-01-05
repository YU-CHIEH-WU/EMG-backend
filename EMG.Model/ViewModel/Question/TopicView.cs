using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMG.Model
{
    public class TopicView
    {
        [DisplayName("題目編號：")]
        public int TopicNo { get; set; }

        [DisplayName("問卷編號：")]
        public int QuestionNo {get;set;}

        [DisplayName("題目：")]
        public string TopicTitle { get; set; }
    }
}
