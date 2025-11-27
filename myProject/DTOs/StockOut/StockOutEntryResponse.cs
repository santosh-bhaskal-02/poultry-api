namespace MyProject.DTOs.StockOut
{
    // -------------------- ENTRY RESPONSE DTO --------------------
    public class StockOutEntryResponseDto
    {
        public int Id { get; set; }
        public int SrNo { get; set; }
        public int Birds { get; set; }
        public decimal Weight { get; set; }
    }

    // -------------------- MASTER RESPONSE DTO --------------------
    public class StockOutMasterResponseDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int BatchId { get; set; }

        public int StockOutNo { get; set; }

        public int TotalBirds { get; set; }

        public decimal TotalWeight { get; set; }

        public decimal AvgWeight { get; set; }

        public List<StockOutEntryResponseDto> Entries { get; set; }
            = new();
    }
}

