using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;

namespace BidHub_Poject.Models
{
   
    public class BidHubDbContext : IdentityDbContext<Users, Roles, Guid>

    {
        public BidHubDbContext(DbContextOptions<BidHubDbContext> options) : base(options)
        {
        }

        public DbSet<Roles>Roles { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<Bidders> Bidders { get; set; }
        public DbSet<Auctioneers> Auctioneers { get; set; }
        public DbSet<UserOtp> UserOtps { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductPhotos> ProductPhotos { get; set; }
        public DbSet<ProductDocuments> ProductDocuments { get; set; }
        public DbSet<BViewing> BViewings { get; set; }
        public DbSet<BidDates> BidDates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Important for configuring Identity tables
            //Identity Role
            modelBuilder.Entity<Roles>().Property(r => r.Role).HasColumnName("Role");
            modelBuilder.Entity<Roles>().Property(r => r.RoleDescription).HasColumnName("RoleDescription");
           
            // Relationships
            // Product and Auctioneer one-to-many
            modelBuilder.Entity<Products>().HasOne(p => p.Auctioneers) .WithMany(a => a.Products).HasForeignKey(p => p.AuctioneerId);
         
            // Bidder and User one-to-many
             modelBuilder.Entity<Bidders>().HasOne(b => b.User) .WithMany(u => u.Bidders) .HasForeignKey(b => b.UserId);
            
            // Auctioneer and User one-to-many
            modelBuilder.Entity<Auctioneers>().HasOne(a => a.User) .WithMany(u => u.Auctioneers).HasForeignKey(a => a.UserId);
           
            // Bidder and User one-to-many
            modelBuilder.Entity<BViewing>() .HasOne(v => v.User) .WithMany(u => u.BViewings) .HasForeignKey(v => v.UserId).OnDelete(DeleteBehavior.Restrict);
            
            // Bidder and User one-to-many
            modelBuilder.Entity<Auctioneers>() .HasOne(a => a.Company) .WithMany(c => c.Auctioneers) .HasForeignKey(a => a.CompanyId) .OnDelete(DeleteBehavior.Cascade);
           
            // Bidder and User one-to-many
            modelBuilder.Entity<UserRoles>() .HasOne(ur => ur.User).WithMany(u => u.UserRoles) .HasForeignKey(ur => ur.UserId);
            
            // Product and Photo one-to-one
            modelBuilder.Entity<ProductPhotos>().HasOne(p => p.Product).WithMany(pp => pp.Photos).HasForeignKey(p => p.ProductId);

            // Product and Document one-to-one
            modelBuilder.Entity<ProductDocuments>().HasOne(p => p.Product).WithMany(pd => pd.Documents).HasForeignKey(p => p.ProductId);

            //modelBuilder.Entity<Products>().HasMany(x => x.Documents).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);

            // Viewing and Product one-to-many
            modelBuilder.Entity<BViewing>().HasOne(bv => bv.Product).WithMany(p => p.BViewings).HasForeignKey(bv => bv.ProductId);

            //BidDate and Product one-to-many
            modelBuilder.Entity<BidDates>().HasOne(bd => bd.Product).WithMany(p => p.BidDates).HasForeignKey(bd => bd.ProductId);

        }
    }
}
