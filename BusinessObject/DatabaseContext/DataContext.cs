using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DatabaseContext
{
    public class DataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options)
           : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Ship>(entity =>
            {
                entity.HasOne(r => r.Book)
                      .WithOne()
                      .HasForeignKey<Ship>(r => r.BookId);
            });

            builder.Entity<Ship>()
               .HasOne(s => s.UserOrder)
               .WithMany()
               .HasForeignKey(s => s.UserOrderId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Ship>()
                .HasOne(s => s.UserShip)
                .WithMany()
                .HasForeignKey(s => s.UserShipId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
