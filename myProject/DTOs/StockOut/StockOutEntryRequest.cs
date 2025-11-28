namespace MyProject.DTOs.StockOut
{
    // -------------------- CREATE MASTER REQUEST --------------------
    public class StockOutMasterCreateDto
    {
        public DateTime Date { get; set; }

        // Refers to Batch table → Batch.Id (not batchNo from UI)
        public int BatchId { get; set; }

        public int StockOutNo { get; set; }

        // Only entries to create along with master
        public List<StockOutEntryCreateDto> Entries { get; set; } = new();
    }

    // -------------------- ENTRY CREATE DTO --------------------
    public class StockOutEntryCreateDto
    {
        // Entry links to master
        public int StockOutMasterId { get; set; }

        public int Birds { get; set; }

        public decimal Weight { get; set; }
    }

    // -------------------- FINALIZE MASTER REQUEST --------------------
    public class StockOutMasterFinalizeDto
    {
        public DateTime Date { get; set; }

        // Refers to Batch table → Batch.Id (not batchNo from UI)
        //public int BatchId { get; set; }

        //public int StockOutNo { get; set; }
        public int TotalBirds { get; set; }
        //public int MasterId { get; set; }

        public decimal TotalWeight { get; set; }

        public decimal AvgWeight { get; set; }
    }



}
