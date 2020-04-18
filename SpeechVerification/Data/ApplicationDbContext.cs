using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpeechVerification.Models;

namespace SpeechVerification.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> ExtUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Ignore<IdentityUserLogin<String>>();
            modelBuilder.Ignore<IdentityUserRole<String>>();
            modelBuilder.Ignore<IdentityUserClaim<String>>();
            modelBuilder.Ignore<IdentityUserToken<String>>();
            modelBuilder.Ignore<IdentityUser<String>>();

            modelBuilder.Entity<User>().HasKey(m => m.ProfileId);
            modelBuilder.Entity<User>().ToTable("ExtUser");;

            base.OnModelCreating(modelBuilder);
        }
    }
}
