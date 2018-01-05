using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMG.Model
{
    public class AnswerView
    {
        public int QuestionNo { get; set; }

        public int TopicNo { get; set; }

        public int SelectNo { get; set; }

        public string Account { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
