namespace MyProject.DTOs.StockOut
{
    public class UpdateStockOutEntryRequest
    {
        public int EntryId { get; set; }
        public int Birds { get; set; }
        public decimal Weight { get; set; }
    }
}
