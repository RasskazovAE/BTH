﻿using Microsoft.EntityFrameworkCore;

namespace BHT.Core.Entities
{
    public class DataContext : DbContext
    {
        public DbSet<CoBaTransaction> CoBaTransactions { get; set; }

        public DataContext()
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($@"Data Source=.\database.db");
        }
    }
}
