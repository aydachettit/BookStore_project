using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Author> Authors{ get; set; }
       
        public DbSet<Publisher> Publishers{ get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Shipment> Shipments { get; set; }

        public DbSet<Bill> Bills { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<BillDetail> BillDetail { get; set; }
        public DbSet<Import> Imports { get; set; }
        public DbSet<ImportDetail> ImportDetails { get; set; }
        public DbSet<ProductDetail> ProductDetail { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN".ToUpper()
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER".ToUpper()
                }
            );

            var hasher = new PasswordHasher<IdentityUser>();
            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "1",
                    UserName = "Nam",
                    Email = "nam@gmail.com",
                    NormalizedUserName = "Nam".ToUpper(),
                    NormalizedEmail = "nam@gmail.com".ToUpper(),
                    PasswordHash = hasher.HashPassword(null, "Admin@123")
                },
                 new IdentityUser
                 {
                     Id = "2",
                     UserName = "Admin",
                     Email = "admin@gmail.com",
                     NormalizedUserName = "Admin".ToUpper(),
                     NormalizedEmail = "admin@gmail.com".ToUpper(),
                     PasswordHash = hasher.HashPassword(null, "Admin@123")
                 }
            );
            modelBuilder.Entity<IdentityUserRole<string>>().HasData
            (
                new IdentityUserRole<string>
                {
                    UserId = "1",
                    RoleId = "2"
                },
                 new IdentityUserRole<string>
                 {
                     UserId = "2",
                     RoleId = "1"
                 }
            );
        }

    }
}
