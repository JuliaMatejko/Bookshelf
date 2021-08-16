using Bookshelf.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookshelf.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Keyword> Kewords { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<AuthorBook> AuthorsBooks { get; set; }
        public DbSet<BookKeyword> BooksKeywords { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserBook> UsersBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AuthorBook>()
                .HasKey(k => new { k.AuthorID, k.BookID });

            modelBuilder.Entity<BookKeyword>()
               .HasKey(k => new { k.BookID, k.KeywordID });

            modelBuilder.Entity<UserBook>()
               .HasKey(k => new { k.ApplicationUserID, k.BookID });

            modelBuilder.Entity<UserBook>()
                .HasOne(ub => ub.ApplicationUser)
                .WithMany(au => au.UserBooks)
                .HasForeignKey(ub => ub.ApplicationUserID);

            modelBuilder.Entity<UserBook>()
                .HasOne(ub => ub.Book)
                .WithMany(b => b.UserBooks)
                .HasForeignKey(ub => ub.BookID);
        }
    }
}
