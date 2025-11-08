using MyProject.Models;

namespace MyProject.Models
{
    public enum BatchStatus
    {
        Ongoing,
        Completed
    }

    public class BirdInventory
    {
        public int Id { get; set; }
        public int BatchNo { get; set; }
        public DateTime Date { get; set; }
        public int BoxCount { get; set; }
        public int BirdsPerBoxCount { get; set; }
        public int TotalBirdCount { get; set; }
        public int BirdsArrivedCount { get; set; }
        public int BoxMortalityCount { get; set; }
        public int DisabledBirdCount { get; set; } 
        public int WeakBirdCount { get; set; }
        public int ExcessBirdCount { get; set; }
        public int HousedBirdCount { get; set; }
        public BatchStatus Status { get; set; } = BatchStatus.Ongoing;
        public ICollection<DailyRecord>? DailyRecords { get; set; }
        public ICollection<FeedInventory>? FeedInventories { get; set; }
        public bool? IsDeleted { get; set; } = false;
    }

}
