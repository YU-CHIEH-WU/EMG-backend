using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMG.Model.ViewModel
{
    public class CkView
    {
        public string Title { get; set; }
        public string Contents { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string Account { get; set; }

        public virtual User User { get; set; }
    }
}
