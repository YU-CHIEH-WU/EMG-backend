using EMG.Model;
using System.Collections.Generic;
using System.Linq;

namespace EMG.Interface
{
    public interface IMessageServices
    {
        bool CheckUpdate(int mId);

        void Delete(Message data);

        IQueryable<Message> GetAllDataList(ForPaging paging, string search);

        IQueryable<Message> GetAllDataList(ForPaging paging);

        Message GetDataById(int mId);

        List<Message> GetDataList(ForPaging paging, string search);

        List<Message> GetIndex();

        void Insert(Message data);

        void Update(Message data);


        /*API*/
        IEnumerable<MessageView> GetAll();

        IEnumerable<MessageView> GetAllByAccount(string account);

        MessageView GetOne(string Id);

        void CreateApi(MessageView data);

        void EditApi(MessageView data);

        void DeleteApi(string Id);
    }
}