using EMG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMG.Interface
{
    public interface IAlbumServices
    {
        List<PhotoListView> getRank();

        List<Photo> GetPersonPhoto(string account);

        void UploadPhoto(HttpPostedFile File);

        void DeletePhoto(int P_Id);
    }
}