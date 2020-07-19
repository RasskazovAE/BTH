using BHT.Core.Entities;
using BTH.Core.DbSupport;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var prop in entity.GetProperties())
                {
                    var attr = prop.PropertyInfo.GetCustomAttribute<IndexAttribute>();
                    if (attr != null)
                    {
                        var index = entity.AddIndex(prop);
                        index.IsUnique = attr.IsUnique;
                        //index.SqlServer().IsClustered = attr.IsClustered;
                    }
                }
            }
        }
    }
}
