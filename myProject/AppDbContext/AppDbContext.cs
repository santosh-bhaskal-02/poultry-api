using Microsoft.EntityFrameworkCore;
using MyProject.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace MyProject.AppDbContextNameSpace
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Batch> Batch { get; set; }
        public DbSet<BirdInventory> BirdInventory { get; set; }
        public DbSet<DailyRecord> DailyRecord { get; set; }
        public DbSet<FeedInventory> FeedInventory { get; set; }
        public DbSet<FinalReport> FinalReport { get; set; }

        // NEW tables
        public DbSet<StockOutMaster> StockOutMaster { get; set; }
        public DbSet<StockOutEntry> StockOutEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ----------- Table Names -------------
            modelBuilder.Entity<Batch>().ToTable("tbl_batch");
            modelBuilder.Entity<BirdInventory>().ToTable("tbl_birdInventory");
            modelBuilder.Entity<DailyRecord>().ToTable("tbl_dailyRegister");
            modelBuilder.Entity<FeedInventory>().ToTable("tbl_feedInventory");
            modelBuilder.Entity<FinalReport>().ToTable("tbl_finalReport");

            modelBuilder.Entity<StockOutMaster>().ToTable("tbl_stockOut");
            modelBuilder.Entity<StockOutEntry>().ToTable("tbl_stockOutEntry");

            // ----------- FINAL REPORT -------------
            modelBuilder.Entity<FinalReport>()
                .HasOne(fr => fr.Batch)
                .WithOne()
                .HasForeignKey<FinalReport>(fr => fr.BatchId)
                .OnDelete(DeleteBehavior.Restrict);

            // ----------- BIRD INVENTORY -------------
            modelBuilder.Entity<BirdInventory>()
                .HasOne(b => b.Batch)
                .WithOne(batch => batch.BirdInventory)
                .HasForeignKey<BirdInventory>(b => b.BatchId)
                .OnDelete(DeleteBehavior.Cascade);

            // ----------- DAILY RECORD -------------
            modelBuilder.Entity<DailyRecord>()
                .HasOne(dr => dr.Batch)
                .WithMany(batch => batch.DailyRecords)
                .HasForeignKey(dr => dr.BatchId)
                .OnDelete(DeleteBehavior.Cascade);

            // ----------- FEED INVENTORY -------------
            modelBuilder.Entity<FeedInventory>()
                .HasOne(fi => fi.Batch)
                .WithMany(batch => batch.FeedInventories)
                .HasForeignKey(fi => fi.BatchId)
                .OnDelete(DeleteBehavior.Cascade);

            // ------------------ STOCKOUT MASTER ------------------
            modelBuilder.Entity<StockOutMaster>()
              
                .HasOne(m => m.Batch)
                .WithMany(b => b.StockOuts)
                .HasForeignKey(m => m.BatchId)
                .OnDelete(DeleteBehavior.Restrict);

            // ------------------ STOCKOUT ENTRY ------------------
            modelBuilder.Entity<StockOutEntry>()
               
                .HasOne(e => e.StockOut)
                .WithMany(m => m.Entries)
                .HasForeignKey(e => e.StockOutMasterId)
                .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);
        }
    }
}
