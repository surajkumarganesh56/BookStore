using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Store.Entities;
using System.Collections.Generic;
using User.Entities;

namespace Book.Store.Core.Repository
{
    public class BookStoreDBContext : DbContext
    {
        public BookStoreDBContext()
        {
        }
        public DbSet<EUser> Users { get; set; }
        public DbSet<EBook> Books { get; set; }
        public BookStoreDBContext(DbContextOptions<BookStoreDBContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("BookStoreDBConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}