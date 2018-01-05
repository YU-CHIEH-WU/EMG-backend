using EMG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMG.Interface
{
    public interface IChartServices
    {
        //APP
        List<Account_Course> getAccountCourseAPP(string account);

        List<EMGData> getEMGDataAPP(string account, DateTime date, int Id);

        List<RecordEffect> getEffectDataAPP(string account, DateTime startDate, DateTime endDate);
    }
}