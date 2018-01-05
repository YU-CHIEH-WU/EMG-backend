using EMG.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMG.Interface
{
    public interface IFameServices 
    {
        List<FameView> getFame();
    }
}