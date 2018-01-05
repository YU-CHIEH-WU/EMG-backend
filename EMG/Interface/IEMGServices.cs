using EMG.Model;
using EMG.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMG.Interface
{
    public interface IEMGServices
    {
        /*API*/
        /*void EMGCreateApi(EMGDataView data);

        void IEMGCreateApi(IEMGView data);

        void RMSCreateApi(RMSView data);*/

        void GetData(EMGView data);

        void saveRest_Max(Rest_Max data);

        List<OutputView> getOutput();
    }
}