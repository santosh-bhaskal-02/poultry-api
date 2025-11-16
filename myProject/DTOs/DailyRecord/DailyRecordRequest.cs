
namespace MyProject.DTOs.DailyRecord
{
    public class DailyRecordRequest
    {
        public int Id { get; set; }

     

        public DateTime Date { get; set; }
        public int BirdAgeInDays { get; set; }
        public int FeedConsumedBags { get; set; }
        public int MortalityCount { get; set; }

        public bool? IsDeleted { get; set; } = false;
    }
}
