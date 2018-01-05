using System;
using System.Data.Entity;

namespace EMG.Model
{
    public class DbContextFactory : IDbContextFactory

    {
        private readonly string _ConnectionString;

        private DbContext dbContext;

        public DbContextFactory(string connectionString)
        {
            this._ConnectionString = connectionString;
        }

        private DbContext DbContext
        {
            get
            {
                if (this.dbContext == null)
                {
                    Type t = typeof(DbContext);
                    this.dbContext =
                        (DbContext)Activator.CreateInstance(t, this._ConnectionString);
                }
                return dbContext;
            }
        }

        public DbContext GetDbContext()
        {
            return this.DbContext;
        }
    }
}
