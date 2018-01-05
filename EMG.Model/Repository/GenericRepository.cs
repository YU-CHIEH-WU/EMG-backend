using System;
using System.Linq;
using System.Data.Entity;

namespace EMG.Model
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private DbContext Context { get; set; }

        private DbSet<T> DbSet { get; set; }

        public GenericRepository(IUnitOfWork uow)
        {
            this.Context = uow.Context;
            this.DbSet = Context.Set<T>();
        }
        public void Create(T instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                DbSet.Add(instance);
            }
        }

        public void Update(T instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                Context.Entry<T>(instance).State = EntityState.Modified;
            }
        }

        public void Delete(T instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                this.Context.Entry<T>(instance).State = EntityState.Deleted;
            }
        }

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return DbSet.AsQueryable();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.Context != null)
                {
                    this.Context.Dispose();
                    this.Context = null;
                }
            }
        }
    }
}
