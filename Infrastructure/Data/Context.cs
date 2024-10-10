using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //optionsBuilder.UseSqlServer("YourConnectionString");
        //}

        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API ile ilişki, validasyon vb. yapılandırmalar
            //modelBuilder.Entity<Product>()
            //    .HasKey(p => p.Id);

            //modelBuilder.Entity<Customer>()
            //    .HasMany(c => c.Orders)
            //    .WithOne(o => o.Customer)
            //    .HasForeignKey(o => o.CustomerId);

            // Diğer konfigürasyonlar...
        }
    }
}
