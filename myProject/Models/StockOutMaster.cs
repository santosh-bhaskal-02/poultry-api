namespace MyProject.Models
{
    public class StockOutMaster
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int BatchId { get; set; }          // FK (correct way)
        public Batch Batch { get; set; }          // Navigation

        public int StockOutNo { get; set; }

        public int TotalBirds { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal AvgWeight { get; set; }

        public ICollection<StockOutEntry> Entries { get; set; }
    }
}
