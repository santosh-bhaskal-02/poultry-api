namespace MyProject.DTOs.FinalReport
{
   
        public class FinalReportRequest
        {
            public int BatchId { get; set; }

            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }

            // Bird
            public int TotalBirds { get; set; }
            public decimal BirdRate { get; set; }
            public decimal BirdCost { get; set; }

            // Feed
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

            // Rearing Charges
            public decimal RearingChargesStd { get; set; }
            public decimal ProductionCostIncentive { get; set; }
            public decimal RearingChargesPerBird { get; set; }
            public decimal TotalRearingCharges { get; set; }

            public decimal TotalAmountPayable { get; set; }
        

    }
}
