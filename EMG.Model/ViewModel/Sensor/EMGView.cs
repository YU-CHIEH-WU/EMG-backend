using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMG.Model.ViewModel
{
    public class EMGView
    {
        public string Account { get; set; }

        public string C_Id { get; set; }

        public DateTime Date { get; set; }

        public string P_Name { get; set; }

        public string Part { get; set; }

        public string SeriesString { get; set; }

        [DisplayName("次數：")]
        public int Times { get; set; }

        [DisplayName("類型：")]
        public int type { get; set; }

        [DisplayName("EMG：")]
        public string EMG { get; set; }

        [DisplayName("IEMG：")]
        public string IEMG { get; set; }

        [DisplayName("RMS：")]
        public string RMS { get; set; }

        [DisplayName("%MVC：")]
        public string PMVC { get; set; }

        public int Id { get; set; }
    }
}
