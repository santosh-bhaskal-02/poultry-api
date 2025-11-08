using MyProject.Models;

namespace MyProject.Models
{
    public class DailyRecord
    {
        public int Id { get; set; }
        public int BatchNo { get; set; }
        public DateTime Date { get; set; }   
        public int BirdAgeInDays { get; set; }
        public int FeedConsumedBags { get; set; }
        public int MortalityCount { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public BirdInventory? BirdInventory { get; set; }
    }
}
