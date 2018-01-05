using EMG.Model;
using System.Collections.Generic;
using System.Linq;

namespace EMG.Interface
{
    public interface IReplyServices
    {
        void Delete(Reply data);

        bool CheckUpdate(int rId);

        Reply GetDataById(int rId);

        List<Reply> GetDataList(ForPaging paging, int mId);

        IQueryable<Reply> GetAllDataList(ForPaging paging, int mId);

        void Insert(Reply data);

        void Update(Reply data);


        /*API*/
        IEnumerable<ReplyView> GetAll(string Id);

        void CreateApi(ReplyView data);

        void DeleteApi(string Id);
    }
}