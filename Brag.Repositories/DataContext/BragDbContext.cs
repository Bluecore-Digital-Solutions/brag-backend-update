
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Brag.Repositories.DataContext
{
    public class BragDbContext : IdentityDbContext<ApplicationUsers>
    {
        public BragDbContext(DbContextOptions<BragDbContext> option) : base(option)
        {

        }

        //Define your Tables here 
        //public DbSet<UserProfile> UserProfiles { get; set; }
         
         

        //protected override void OnModelCreating(ModelBuilder builder)
        //{

        //    buildroer.Entity<ApplicationUsers>(entity =>
        //    {
        //        entity.ToTable(name: "AspNetUsers");
        //    });

        //    builder.Entity<IdentityRole>(entity =>
        //    {
        //        entity.ToTable(name: "AspNetUsersRoles");
        //    });
        //    builder.Entity<IdentityUserRole<string>>(entity =>
        //    {
        ////        b.HasMany(e => e.UserRoles)
        ////.WithOne(e => e.User)
        ////.HasForeignKey(ur => ur.UserId)
        ////.IsRequired();

        //        entity.HasNoKey();
        //        entity.ToTable("AspNetUsersUserRoles");
        //    });

        //    builder.Entity<IdentityUserClaim<string>>(entity =>
        //    {
        //        entity.ToTable("AspNetUsersUserClaims");
        //    });

        //    builder.Entity<IdentityUserLogin<string>>(entity =>
        //    {
        //        entity.HasNoKey();
        //        entity.ToTable("UserLogins");
        //    });

        //    builder.Entity<IdentityRoleClaim<string>>(entity =>
        //    {
        //        entity.ToTable("AspNetUsersRoleClaims");

        //    });

        //    builder.Entity<IdentityUserToken<string>>(entity =>
        //    {
        //        entity.HasNoKey();
        //        entity.ToTable("AspNetUsersUserTokens");
        //    });
        //}
    }

}
