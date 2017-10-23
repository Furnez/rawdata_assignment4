using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Assignment_4
{
    public class NorthwindContext : DbContext
    {
        // Public variables
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Orderdetail> Orderdetails { get; set; }

        // Private variables
        private string _connectionname;
        private string _dbname;
        private string _uid;
        private string _pwd;

        // Constructor with arguments so that the connection to the database is more modular
        public NorthwindContext(string connection_name, string dbname, string uid, string pwd)
        {
            this._connectionname = connection_name;
            this._dbname = dbname;
            this._uid = uid;
            this._pwd = pwd;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql(
                "server=" + this._connectionname +
                ";database=" + this._dbname +
                ";uid=" + this._uid +
                ";pwd=" + this._pwd
                );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
            .Property(x => x.Name).HasColumnName("categoryname");

            modelBuilder.Entity<Product>()
            .Property(x => x.Id).HasColumnName("productid");

            modelBuilder.Entity<Order>()
            .Property(x => x.Id).HasColumnName("orderid");

            modelBuilder.Entity<Orderdetail>()
           .HasKey(c => new { c.Id, c.ProductId });
        }
    }
}
