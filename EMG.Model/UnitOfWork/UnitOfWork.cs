using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace EMG.Model
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextFactory DbContextFactory { get; set; }

        private DbContext _context;

        private Dictionary<Type, object> _repositories;

        private bool _disposed;

        public DbContext Context
        {
            get
            {
                if (this._context != null)
                {
                    return this._context;
                }
                this._context = DbContextFactory.GetDbContext();
                return this._context;
            }
        }

        public UnitOfWork(IDbContextFactory factory)
        {
            this.DbContextFactory = factory;
            this._context = DbContextFactory.GetDbContext();
            this._repositories = new Dictionary<Type, object>();
        }

        public void Save()
        {
            this._context.SaveChanges();
        }

        //清除資源
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    this._context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
