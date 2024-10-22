using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;

namespace BidHub_Poject.Models
{
    public class BidHubDbContext : DbContext
    {
        public BidHubDbContext(DbContextOptions<BidHubDbContext> options)
       : base(options)
        {
        } 
        //public DbSet<Roles> Roles { get; set; }
        //public DbSet<Users> Users { get; set; }
        //public DbSet<UserRoles> UserRoles { get; set; }
        //public DbSet<Bidders> Bidders { get; set; }
        //public DbSet<Auctioneers> Auctioneers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductPhotos> ProductPhotos { get; set; }
        public DbSet<ProductDocuments> ProductDocuments { get; set; }
        public DbSet<BViewing> BViewings { get; set; }
        public DbSet<BidDates> BidDates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relationships

            // User-Role many-to-many relationship
            //modelBuilder.Entity<UserRoles>()
            //    .HasKey(ur => new { ur.UserId, ur.RoleId });

            //modelBuilder.Entity<UserRoles>()
            //    .HasOne(ur => ur.User)
            //    .WithMany(u => u.UserRoles)
            //    .HasForeignKey(ur => ur.UserId);

            //modelBuilder.Entity<UserRoles>()
            //    .HasOne(ur => ur.Role)
            //    .WithMany(r => r.UserRoles)
            //    .HasForeignKey(ur => ur.RoleId);

            // Auctioneer and Company one-to-many
            //modelBuilder.Entity<Auctioneers>()
            //    .HasOne(a => a.Company)
            //    .WithMany(c => c.Auctioneers)
            //    .HasForeignKey(a => a.CompanyId);

            //// Auctioneer and Product one-to-many
            //modelBuilder.Entity<Auctioneers>()
            //    .HasOne(a => a.Product)
            //    .WithMany(p => p.Auctioneers)
            //    .HasForeignKey(a => a.ProductId);

            //modelBuilder.Entity<ProductDocument>().HasOne(pd => pd.Product).WithMany(p => p.ProductDocuments).HasForeignKey(pd => pd.ProductId);

            //modelBuilder.Entity<ProductPhoto>().HasOne(pp => pp.Product).WithMany(p => p.ProductPhotos).HasForeignKey(pp => pp.ProductId);


            // Product and Photo one-to-one
            modelBuilder.Entity<ProductPhotos>()
                .HasOne(p => p.Product)
                .WithMany(pp => pp.Photos)
                .HasForeignKey(p => p.ProductId);

            // Product and Document one-to-one
            modelBuilder.Entity<ProductDocuments>()
                .HasOne(p => p.Product)
                .WithMany(pd => pd.Documents)
                .HasForeignKey(p => p.ProductId);

            //modelBuilder.Entity<Products>().HasMany(x => x.Documents).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);

            // Bidder and User one-to-one
            //modelBuilder.Entity<Bidders>()
            //    .HasOne(b => b.User)
            //    .WithMany(u => u.Bidders)
            //    .HasForeignKey(b => b.UserId);

            // Viewing and User/Product one-to-many
            //modelBuilder.Entity<BViewing>()
            //    .HasOne(bv => bv.User)
            //    .WithMany(u => u.BViewings)
            //    .HasForeignKey(bv => bv.UserId);

            modelBuilder.Entity<BViewing>()
                .HasOne(bv => bv.Product)
                .WithMany(p => p.BViewings)
                .HasForeignKey(bv => bv.ProductId);

            //BidDate and Product one-to - many
            modelBuilder.Entity<BidDates>()
                .HasOne(bd => bd.Product)
                .WithMany(p => p.BidDates)
                .HasForeignKey(bd => bd.ProductId);

        }
    }
}
