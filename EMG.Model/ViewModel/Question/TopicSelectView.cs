using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMG.Model
{
    public class TopicSelectView
    {
        [DisplayName("問卷編號：")]
        public int QuestionNo { get; set; }

        [DisplayName("題目編號：")]
        public int TopicNo { get; set; }

        [DisplayName("選項編號：")]
        public int SelectNo { get; set; }

        [DisplayName("選項名稱：")]
        public string SelectName { get; set; }
    }
}
