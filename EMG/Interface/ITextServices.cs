using EMG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMG.Interface
{
    public interface ITextServices
    {
        Text GetDataByTitle(string title, Text Data);

        void Insert(Text data);

        //bool CheckUpdate(string title);

        bool Update(Text data);
    }
}