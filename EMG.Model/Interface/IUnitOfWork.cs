using System;
using System.Data.Entity;

namespace EMG.Model
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }

        void Save();

    }
}
