using application.interfaces;
using AutoMapper.Internal.Mappers;
using domain.entities;
using infrastructure.identity;
using infrastructure.persistence.entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.dependency
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        // public ApplicationDbContext() {}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach(var entityType in builder.Model.GetEntityTypes())
            {
                if(typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                {
                    builder.Entity(entityType.ClrType)
                        .HasQueryFilter(ConvertFilterExpression(entityType.ClrType));
                }
            }
            // thay thế schema mặc định
            builder.HasDefaultSchema("MySchema");

            builder.Entity<CategoryEntity>().ToTable("Categories");
            builder.Entity<CategoryEntity>()
                .HasMany(p => p.Products)
                .WithOne(p => p.CategoryEntity)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ReviewEntity>().ToTable("Reviews");

            builder.Entity<AppUser>()
                .ToTable("Users");
            
            builder.Entity<AppUser>()
                .HasMany(a => a.Addresses)
                .WithOne(a => a.AppUser)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AppUser>()
                 .HasMany(o => o.Orders)
                 .WithOne(o => o.AppUser)
                 .HasForeignKey(e => e.UserId)
                 .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<AppUser>()
                .HasMany(c => c.Carts)
                .WithOne(c => c.AppUser)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<AppUser>()
                .HasMany(c => c.Reviews)
                .WithOne(e => e.AppUser)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            
            builder.Entity<AppUser>()
                .Property(e => e.Email)
                .IsRequired();
            
            builder.Entity<CartEntity>()
                .ToTable("Carts");

            builder.Entity<CartEntity>()
                .HasMany(e => e.Items)
                .WithOne(e => e.Cart)
                .HasForeignKey(e => e.CartId)
                .OnDelete(DeleteBehavior.Cascade);
            

            builder.Entity<CartItemEntity>()
                .ToTable("CartItems");

            builder.Entity<CartItemEntity>()
                .Property(e => e.UnitPrice)
                .HasColumnType("decimal(15,2)");
            
            // builder.Entity<AppUser>()
            // .Property(e => e.FirstName)

            builder.Entity<ProductEntity>()
                .ToTable("Products");

            builder.Entity<ProductEntity>()
                .HasMany(r => r.Reviews)
                .WithOne(p => p.ProductEntity)
                .HasForeignKey(p => p.ProductEntityId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ProductEntity>()
                .HasMany(r => r.OrderItems)
                .WithOne(p => p.Products)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ProductEntity>()
                .Property(p => p.Price)
                .HasColumnType("decimal(15,2)")
                .IsRequired();
            
            builder.Entity<ProductEntity>()
                .Property(p => p.SalePrice)
                .HasColumnType("decimal(15,2)").IsRequired();

            builder.Entity<ProductEntity>()
                .Property(p => p.AvgRating)
                .HasColumnType("decimal(15,2)");
            
            builder.Entity<VoucherEntity>()
                .ToTable("Vouchers");
            builder.Entity<VoucherEntity>()
                .HasMany(r => r.Orders)
                .WithOne(p => p.Voucher)
                .HasForeignKey(e => e.VoucherId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<VoucherEntity>()
                .Property(e => e.Value)
                .HasColumnType("decimal(15,2)");
                
            builder.Entity<OrderEntity>()
                .ToTable("Orders");
            builder.Entity<OrderEntity>()
                .HasMany(o => o.Items)
                .WithOne(o => o.Orders)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<OrderEntity>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(15,2)")
                .IsRequired();
            
            builder.Entity<OrderEntity>()
                .Property(o => o.DiscountAmount)
                .HasColumnType("decimal(15,2)");
            
            builder.Entity<OrderEntity>()
                .Property(o => o.ShippingFee)
                .HasColumnType("decimal(15,2)");

            builder.Entity<OrderEntity>()
                .Property(o => o.FinalAmount)
                .HasColumnType("decimal(15,2)")
                .IsRequired();
            
            builder.Entity<OrderItemEntity>()
                .ToTable("OrderItems");

            builder.Entity<ReviewEntity>()
                .Property(e => e.Rating)
                .HasColumnType("decimal(15,2)");

        }
        // public DbSet<AppUser> Users => Set<AppUser>();
        // public DbSet<AppRole> Roles => Set<AppRole>();
        // public DbSet<Products> Products => Set<Products>();
        public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();
        public DbSet<ProductEntity> Products=> Set<ProductEntity>();
        public DbSet<ReviewEntity> Reviews => Set<ReviewEntity>();
        public DbSet<OrderEntity> Orders => Set<OrderEntity>();
        public DbSet<OrderItemEntity> OrderItems => Set<OrderItemEntity>();
        public DbSet<AddressEntity> Addresses=> Set<AddressEntity>();
        public DbSet<VoucherEntity> Vouchers=> Set<VoucherEntity>();
        public DbSet<CartEntity> Carts=> Set<CartEntity>();
        public DbSet<CartItemEntity> CartItems=> Set<CartItemEntity>();
        // public DbSet<WishlistEntity> Wishlists => Set<WishlistEntity>();
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // lấy ra day sách được eeff theo dõi 
            var entities = ChangeTracker.Entries<IBase>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
            foreach(var entry in entities)
            {
                var now = DateTime.UtcNow;
                if(entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                }
                entry.Entity.UpdatedAt = now;
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        private static LambdaExpression ConvertFilterExpression(Type type)
        {
            // tạo tham số x cho biểu thức x => 
            var paramter = Expression.Parameter(type, "x");

            // tạo biểu thức so sánh x.IsDeleted == false
            var property = Expression.Property(paramter, nameof(ISoftDelete.IsDeleted));
            var falseConstant = Expression.Constant(false);
            var comparion = Expression.Equal(property, falseConstant);

            // trả về biểu thức hoàn chình 
            return Expression.Lambda(comparion, paramter);
        }
    }
}
