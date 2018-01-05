using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMG.Model
{
    public class PostId
    {
        public string account { get; set; }

        //留言ID
        public string MId { get; set; }

        //回覆ID
        public string RId { get; set; }

        //EMGID
        public string E_Id { get; set; }

        //IEMGID
        public string I_Id { get; set; }

        //RMSID
        public string R_Id { get; set; }

        //課程ID
        public string C_Id { get; set; }

        //新聞ID
        public string N_Id { get; set; }
    }
}
