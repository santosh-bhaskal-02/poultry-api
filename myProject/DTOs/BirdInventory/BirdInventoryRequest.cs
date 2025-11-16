namespace MyProject.DTOs.BirdInventory
{
    public class BirdInventoryRequest
    {
        public int BatchId { get; set; }
        public int BatchNo { get; set; }
        public DateTime Date { get; set; }
        public int BoxCount { get; set; }
        public int BirdsPerBoxCount { get; set; }
        public int TotalBirdCount { get; set; }
        public int BirdsArrivedCount { get; set; }
        public int BoxMortalityCount { get; set; }
        public int DisabledBirdCount { get; set; }
        public int WeakBirdCount { get; set; }
        public int ShortBirdCount { get; set; }
        public int ExcessBirdCount { get; set; }
        public int HousedBirdCount { get; set; }
    }
}
