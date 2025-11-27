namespace MyProject.Models
{
    public class StockOutEntry
    {
        public int Id { get; set; }

        public int SrNo { get; set; }
        public int Birds { get; set; }
        public decimal Weight { get; set; }

        public int StockOutMasterId { get; set; }         // FK
        public StockOutMaster StockOut { get; set; } // Navigation
    }
}
