﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AdaptiveWebProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //public virtual Questions Questions { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public virtual DbSet<Posts> Posts { get; set; }

        public virtual DbSet<UsersData> UsersData { get; set; }

        public virtual DbSet<PostTags> PostTags { get; set; }

        public virtual DbSet<PostDetails> PostDetails { get; set; }

        public virtual DbSet<UserTags> UserTags { get; set; }

        public virtual DbSet<UserSatisfaction> UserSatisfaction { get; set; }

        public virtual DbSet<UserName> UserName { get; set; }

        public virtual DbSet<PostTagsImp> PostTagsImp { get; set; }

        public virtual DbSet<UserAcceptedAnswers> UserAcceptedAnswers { get; set; }

        public virtual DbSet<UserUpvotes> UserUpvotes { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Questions>()
        //    .ToTable("AspNetUsers")
        //    .HasRequired(c => c.UserId);
        //}
    }
}