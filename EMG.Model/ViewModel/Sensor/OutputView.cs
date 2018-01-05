using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMG.Model
{
    public class OutputView
    {
        public string Account { get; set; }

        public string C_Id { get; set; }

        public DateTime Date { get; set; }

        public string P_Name { get; set; }


        public int Series { get; set; }

        [DisplayName("次數：")]
        public int Times { get; set; }

        [DisplayName("類型：")]
        public int type { get; set; }

        public int PMVC { get; set; }
    }
}
