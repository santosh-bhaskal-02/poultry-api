using Microsoft.EntityFrameworkCore;
using myProject.Models;

namespace myProject.dbContextNameSpace
{
    public class dbContext :DbContext
    {

        public dbContext(DbContextOptions _dbContext):base(_dbContext) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyRegister>().ToTable("tbl_dailyRegister");
            modelBuilder.Entity<BirdInventory>().ToTable("tbl_birdInventory");
            modelBuilder.Entity<FeedInventory>().ToTable("tbl_feedInventory");
        }

        public DbSet<DailyRegister> DailyRegister { get; set; }
        public DbSet<FeedInventory> FeedInventory { get; set; }
        public DbSet<BirdInventory> BirdInventory { get; set; }

    }
}
