using MyProject.Models;

namespace MyProject.Models
{
    public class FeedInventory
    {
        public int Id { get; set; }
        public int BatchNo { get; set; }
        public DateTime Date { get; set; }
        public string FeedName { get; set; } = string.Empty;
        public int BagsArrivedCount { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public string DriverPhoneNumber { get; set; } = string.Empty;
        public BirdInventory? BirdInventory { get; set; }
        public bool? IsDeleted { get; set; } = false;
    }
}
