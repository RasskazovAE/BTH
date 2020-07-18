using BHT.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BTH.Core.Context
{
    public class DataContext : DbContext
    {
        public DbSet<CoBaTransaction> CoBaTransactions { get; set; }

        public DataContext()
            : this(new DbContextOptionsBuilder()
                  .UseSqlite($@"Data Source=.\database.db")
                  .Options)
        {
        }

        public DataContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }
    }
}
