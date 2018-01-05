using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMG.Model.ViewModel
{
    public class SaveCourseView
    {
        public List<Course> courseList { get; set; }

        public string account { get; set; }

        public DateTime date { get; set; }

        public string goal { get; set; }
    }

    public class SaveCourseViewAPP
    {
        public string C_Id { get; set; }

        public string account { get; set; }

        public DateTime date { get; set; }

        public string goal { get; set; }
    }
}
