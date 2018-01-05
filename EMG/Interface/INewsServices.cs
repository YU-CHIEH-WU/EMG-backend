using EMG.Model;
using EMG.Model.ViewModel.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMG.Interface
{
    public interface INewsServices
    {
        News GetDataById(int nId);

        IEnumerable<NewsView> GetAll();

        void Create(NewsView newData);

        void Edit(NewsView nowData);

        void Delete(string n_Id);
    }
}