using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NextJsWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NextJsWebAPI.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(x =>
            {
               x.ToTable("User");
               x.HasKey(i => i.Id);
            });

            builder.Entity<Role>(x =>
            {
                x.ToTable("Role");
                x.HasKey(i => i.Id);
            });

            builder.Entity<Category>(x =>
            {
                x.ToTable("Category");
                x.HasKey(i => i.Id);
            });

            builder.Entity<IdentityUserRole<string>>(u =>
            {
                u.ToTable("UserRole");
                u.HasKey(x => new { x.RoleId, x.UserId });
            });
            builder.Entity<IdentityUserClaim<string>>(u =>
            {
                u.ToTable("UserClaim");
                u.HasKey(x => x.Id);
            });
            builder.Entity<IdentityUserLogin<string>>(u =>
            {
                u.ToTable("UserLogin");
                u.HasKey(x => new { x.ProviderKey, x.LoginProvider });
            });
        }

    }
}
