using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoreLibrary.Areas.Identity.Data;
using StoreLibrary.Models;

namespace StoreLibrary.Areas.Identity.Data;

public class StoreLibraryContext : IdentityDbContext<StoreLibraryUser>
{
    public StoreLibraryContext(DbContextOptions<StoreLibraryContext> options)
        : base(options)
    {
    }
    public DbSet<Store> Store { get; set; }
    public DbSet<Book> Book { get; set; }
    public DbSet<Category> Category { get; set; } = null!;
    public DbSet<Order> Order { get; set; }
    public DbSet<OrderDetail> OrderDetail { get; set; }
    public DbSet<Cart> Cart { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        base.OnModelCreating(builder);
        base.OnModelCreating(builder);
        builder.Entity<StoreLibraryUser>()
            .HasOne<Store>(au => au.Store)
            .WithOne(st => st.User)
            .HasForeignKey<Store>(st => st.UId);

        builder.Entity<Book>()
            .HasOne<Store>(b => b.Store)
            .WithMany(st => st.Books)
            .HasForeignKey(b => b.StoreId);

        builder.Entity<Book>()
            .HasOne<Category>(b => b.Category)
            .WithMany(c => c.Books)
            .HasForeignKey(c => c.CategoryId);

        builder.Entity<Order>()
            .HasOne<StoreLibraryUser>(o => o.User)
            .WithMany(ap => ap.Orders)
            .HasForeignKey(o => o.UId);

        builder.Entity<OrderDetail>()
            .HasKey(od => new { od.OrderId, od.BookIsbn });
        builder.Entity<OrderDetail>()
            .HasOne<Order>(od => od.Order)
            .WithMany(or => or.OrderDetails)
            .HasForeignKey(od => od.OrderId);
        builder.Entity<OrderDetail>()
            .HasOne<Book>(od => od.Book)
            .WithMany(b => b.OrderDetails)
            .HasForeignKey(od => od.BookIsbn);

        builder.Entity<Cart>()
            .HasKey(c => new { c.UId, c.BookIsbn });
        builder.Entity<Cart>()
            .HasOne<StoreLibraryUser>(c => c.User)
            .WithMany(u => u.Carts)
            .HasForeignKey(c => c.UId);
        builder.Entity<Cart>()
            .HasOne<Book>(od => od.Book)
            .WithMany(b => b.Carts)
            .HasForeignKey(od => od.BookIsbn)
            .OnDelete(DeleteBehavior.NoAction);

    }
}
