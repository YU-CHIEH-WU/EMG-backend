using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMG.Model
{
    public class FileListView
    {
        public string Search { get; set; }

        public ForPaging Paging { get; set; }

        public List<FileView> FirstFile { get; set; }
    }
}
