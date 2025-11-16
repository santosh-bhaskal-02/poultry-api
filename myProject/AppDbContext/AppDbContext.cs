using Microsoft.EntityFrameworkCore;
using MyProject.Models;

namespace MyProject.AppDbContextNameSpace
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Batch> Batch { get; set; }
        public DbSet<BirdInventory> BirdInventory { get; set; }
        public DbSet<DailyRecord> DailyRecord { get; set; }
        public DbSet<FeedInventory> FeedInventory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Batch>().ToTable("tbl_batch");
            modelBuilder.Entity<BirdInventory>().ToTable("tbl_birdInventory");
            modelBuilder.Entity<DailyRecord>().ToTable("tbl_dailyRegister");
            modelBuilder.Entity<FeedInventory>().ToTable("tbl_feedInventory");

            modelBuilder.Entity<BirdInventory>()
                .HasOne(b => b.Batch)
                .WithOne(batch => batch.BirdInventory)
                .HasForeignKey<BirdInventory>(b => b.BatchId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DailyRecord>()
                .HasOne(dr => dr.Batch)
                .WithMany(batch => batch.DailyRecords)
                .HasForeignKey(dr => dr.BatchId)
                .OnDelete(DeleteBehavior.Cascade);

        
            modelBuilder.Entity<FeedInventory>()
                .HasOne(fi => fi.Batch)
                .WithMany(batch => batch.FeedInventories)
                .HasForeignKey(fi => fi.BatchId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
