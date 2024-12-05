using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MyProject.Model;
using System;

namespace MyProject.Context
{
    public class DatabaseContext : IdentityDbContext<User>
    {

        private IWebHostEnvironment _environment;

        public DbSet<Chat> Chats { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostComment> PostComments { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options, IWebHostEnvironment environment) : base(options)
        {
            _environment = environment;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        {
            var folder = Path.Combine(_environment.WebRootPath, "database");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            optionbuilder.UseSqlite($"Data Source={folder}/database.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Chat>(chat =>
            {
                chat.HasOne(chat => chat.ToUser)
                    .WithMany(user => user.ReceivedChats)
                    .HasForeignKey(chat => chat.ToUserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                chat.HasOne(chat => chat.FromUser)
                    .WithMany(user => user.SentChats)
                    .HasForeignKey(chat => chat.FromUserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Follower>(follower =>
            {
                follower.HasOne(follower => follower.User)
                    .WithMany(user => user.FollowedUsers)
                    .HasForeignKey(follower => follower.FollowingUserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                follower.HasOne(follower => follower.FollowingUser)
                    .WithMany(user => user.FollowingUsers)
                    .HasForeignKey(follower => follower.UserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });

        }
    }
}