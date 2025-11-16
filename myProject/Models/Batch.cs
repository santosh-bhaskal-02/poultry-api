namespace MyProject.Models
{

    public enum BatchStatus
    {
        Ongoing,
        Completed
    }
    public class Batch
    {
        public int Id { get; set; }
        public int BatchNo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public BatchStatus Status { get; set; } = BatchStatus.Ongoing;

        public BirdInventory? BirdInventory { get; set; }
        public ICollection<DailyRecord>? DailyRecords { get; set; }
        public ICollection<FeedInventory>? FeedInventories { get; set; }
        public bool IsDeleted { get; set; } = false;
    }


}
