using EMG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMG.Interface
{
    public interface IFileServices
    {
        void Create(string fileName, string type, string path, int size);

        void Update(int id, string fileName, int editionCount, string type, string path, int size);

        void Delete(int id, int edition);

        void DeleteConfirm(int id, int edition);

        File Get(int id, int edition);

        List<File> GetAll();

        List<File> GetEdition(int id);

        List<File> GetTrash();

        int GetAllCount();

        int GetEditionCount(int id);
    }
}