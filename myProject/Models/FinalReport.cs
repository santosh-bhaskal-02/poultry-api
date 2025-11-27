namespace MyProject.Models
{
    public class FinalReport
    {
        public int Id { get; set; }

        public int BatchId { get; set; }  // Foreign Key
        public Batch? Batch { get; set; }  // Navigation Property

        // Dates
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Bird quantities
        public int TotalBirds { get; set; }
        public decimal BirdRate { get; set; }
        public decimal BirdCost { get; set; }

        // Feed quantities
        public int TotalBags { get; set; }
        public decimal TotalFeedKg { get; set; }
        public decimal FeedRate { get; set; }
        public decimal FeedCost { get; set; }

        // Stock Out
        public int TotalSoldBirds { get; set; }
        public decimal TotalBirdsWeight { get; set; }
        public decimal AvgBodyWeight { get; set; }

        // Expenses
        public decimal ChicksCost { get; set; }
        public decimal MedicineCost { get; set; }
        public decimal AdminCost { get; set; }
        public decimal GrossProductionCost { get; set; }
        public decimal NetCostPerKg { get; set; }
        public decimal StdCostPerKg { get; set; }

        // Rearing charges
        public decimal RearingChargesStd { get; set; }
        public decimal ProductionCostIncentive { get; set; }
        public decimal RearingChargesPerBird { get; set; }
        public decimal TotalRearingCharges { get; set; }

        // Final Bill
        public decimal TotalAmountPayable { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
