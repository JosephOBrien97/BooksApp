using BooksAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Data;

public class BookAppDb : DbContext
{
    public BookAppDb(DbContextOptions<BookAppDb> options) : base(options)
    {
    }

    public DbSet<AppUser> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Quote> Quotes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure User entity
        modelBuilder.Entity<AppUser>()
            .HasIndex(u => u.Username)
            .IsUnique();

        // Configure Book entity
        modelBuilder.Entity<Book>()
            .HasOne(b => b.User)
            .WithMany(u => u.Books)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Quote entity
        modelBuilder.Entity<Quote>()
            .HasOne(q => q.User)
            .WithMany(u => u.Quotes)
            .HasForeignKey(q => q.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}