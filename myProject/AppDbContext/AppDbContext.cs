using Microsoft.EntityFrameworkCore;
using MyProject.Models;

namespace MyProject.AppDbContextNameSpace
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions _dbContext):base(_dbContext) {

        }

        public DbSet<DailyRecord> DailyRecord { get; set; }
        public DbSet<FeedInventory> FeedInventory { get; set; }
        public DbSet<BirdInventory> BirdInventory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyRecord>().ToTable("tbl_dailyRegister");
            modelBuilder.Entity<BirdInventory>().ToTable("tbl_birdInventory");
            modelBuilder.Entity<FeedInventory>().ToTable("tbl_feedInventory");

            modelBuilder.Entity<BirdInventory>().HasIndex(b => b.BatchNo).IsUnique();

            modelBuilder.Entity<DailyRecord>()
                .HasOne(d => d.BirdInventory)
                .WithMany(b => b.DailyRecords)
                .HasForeignKey(d => d.BatchNo)
                .HasPrincipalKey(b => b.BatchNo)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FeedInventory>()
                .HasOne(d => d.BirdInventory)
                .WithMany(b => b.FeedInventories)
                .HasForeignKey(d => d.BatchNo)
                .HasPrincipalKey(b => b.BatchNo)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
