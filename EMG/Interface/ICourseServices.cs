using EMG.Model;
using EMG.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMG.Interface
{
    public interface ICourseServices
    {
        List<Account_Course> hasCourse(string account);

        List<Account_Course> getCourse(string account);

        List<Course> CreateCourse(CourseSelectView data);

        List<Account_Course> SaveCourse(SaveCourseView data);

        List<Course> getPopular();

        List<Course> getTheme();

        List<Course> getBodyfat();

        Posture getPosData(string P_Name);

        List<Course> getAge(string account);

        List<Course> getSports(string account);

        List<Course> getPlace(string account);

        List<Course> getMotivation(string account);

        List<Course> getSame(string account);

        List<Course> getWatch(string account);

        //APP
        List<Account_Course> getOneCourse(string account);

        List<Account_Course> getCourseList(string account);

        List<Account_Course> getCourseDetail(string account, string C_Id, DateTime Date);

        List<Course> CreateCourseAPP(CourseSelectView data);

        void DeleteCourseAPP(string C_Id);

        List<Account_Course> SaveCourseAPP(SaveCourseViewAPP data);

        void savePower(string account, string C_Id, string P_Name, int? Power);

        void endTrain(Account_Course data);
    }
}

