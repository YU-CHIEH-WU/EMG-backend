using System.Data.Entity;

namespace EMG.Model
{
    public interface IDbContextFactory
    {
        DbContext GetDbContext();
    }
}
