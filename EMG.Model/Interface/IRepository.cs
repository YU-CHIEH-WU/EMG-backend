using System;
using System.Linq;
using System.Linq.Expressions;

namespace EMG.Model
{
    public interface IRepository<T> : IDisposable where T : class
    {
        void Create(T instance);

        void Update(T instance);

        void Delete(T instance);
        //依照主鍵取得
        T Get(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAll();
    }
}
