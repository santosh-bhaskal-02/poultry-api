namespace MyProject.DTOs.DailyRecord
{
    public class DailyRecordUpdateRequest
    {
        
            public DateTime Date { get; set; }
            public int BirdAgeInDays { get; set; }
            public int FeedConsumedBags { get; set; }
            public int MortalityCount { get; set; }
        

    }
}
