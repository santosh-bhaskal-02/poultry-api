
namespace MyProject.DTOs.FeedInventory
{
    public class FeedInventoryRequest
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string FeedName { get; set; } = string.Empty;
        public int BagsArrivedCount { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public string DriverPhoneNumber { get; set; } = string.Empty;

        public bool? IsDeleted { get; set; } = false;
    }
}
