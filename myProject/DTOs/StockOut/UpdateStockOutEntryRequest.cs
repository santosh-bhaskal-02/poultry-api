namespace MyProject.DTOs.StockOut
{
    public class UpdateStockOutEntryRequest
    {
        public int Id { get; set; }
        public int SrNo { get; set; }
        public int Birds { get; set; }
        public decimal Weight { get; set; }
    }
}
