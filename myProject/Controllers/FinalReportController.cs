using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.AppDbContextNameSpace;
using MyProject.Models;
using MyProject.DTOs.FinalReport;
using MyProject.Utilities;

namespace MyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinalReportController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<FinalReportController> _logger;

        public FinalReportController(
            ILogger<FinalReportController> logger,
            AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        // ----------------------- GET ALL -----------------------
        [HttpGet]
        public IActionResult GetAll()
        {
            var batchId = _dbContext.Batch
                .Where(x => !x.IsDeleted && x.Status == BatchStatus.Ongoing)
                .Select(x => x.Id)
                .FirstOrDefault();

            var report = _dbContext.FinalReport
                .Where(x => !x.IsDeleted && x.BatchId == batchId)
                .OrderByDescending(x => x.StartDate)
                .ToList();

            return Ok(new { Message = "Final reports fetched successfully", data = report });
        }

        // ----------------------- CREATE -----------------------
        [HttpPost]
        public IActionResult Create([FromBody] FinalReportRequest req)
        {
            if (req == null)
                return BadRequest(new { Message = "Invalid request data" });

            var batchId = _dbContext.Batch
                .Where(x => !x.IsDeleted && x.Status == BatchStatus.Ongoing)
                .Select(x => x.Id)
                .FirstOrDefault();

            if (batchId == 0)
                return BadRequest(new { Message = "No active batch found." });

            var record = new FinalReport
            {
                BatchId = batchId,
                StartDate = DateTimeHelper.NormalizeToUtc(req.StartDate),
                EndDate = DateTimeHelper.NormalizeToUtc(req.EndDate),

                TotalBirds = req.TotalBirds,
                BirdRate = req.BirdRate,
                BirdCost = req.BirdCost,

                TotalBags = req.TotalBags,
                TotalFeedKg = req.TotalFeedKg,
                FeedRate = req.FeedRate,
                FeedCost = req.FeedCost,

                TotalSoldBirds = req.TotalSoldBirds,
                TotalBirdsWeight = req.TotalBirdsWeight,
                AvgBodyWeight = req.AvgBodyWeight,

                ChicksCost = req.ChicksCost,
                MedicineCost = req.MedicineCost,
                AdminCost = req.AdminCost,
                GrossProductionCost = req.GrossProductionCost,
                NetCostPerKg = req.NetCostPerKg,
                StdCostPerKg = req.StdCostPerKg,

                RearingChargesStd = req.RearingChargesStd,
                ProductionCostIncentive = req.ProductionCostIncentive,
                RearingChargesPerBird = req.RearingChargesPerBird,
                TotalRearingCharges = req.TotalRearingCharges,

                TotalAmountPayable = req.TotalAmountPayable,
                IsDeleted = false
            };

            _dbContext.FinalReport.Add(record);
            _dbContext.SaveChanges();

            _logger.LogInformation("Final report created for batch {BatchId}", batchId);

            return Ok(new
            {
                Message = "Final report created successfully.",
                Data = record
            });
        }

        // ----------------------- UPDATE -----------------------
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] FinalReport req)
        {
            if (id <= 0)
                return BadRequest(new { Message = "Invalid ID" });

            var updated = _dbContext.FinalReport
                .Where(x => x.Id == id && !x.IsDeleted)
                .ExecuteUpdate(setter => setter
                    .SetProperty(x => x.StartDate, req.StartDate)
                    .SetProperty(x => x.EndDate, req.EndDate)
                    .SetProperty(x => x.TotalBirds, req.TotalBirds)
                    .SetProperty(x => x.BirdRate, req.BirdRate)
                    .SetProperty(x => x.BirdCost, req.BirdCost)
                    .SetProperty(x => x.TotalBags, req.TotalBags)
                    .SetProperty(x => x.TotalFeedKg, req.TotalFeedKg)
                    .SetProperty(x => x.FeedRate, req.FeedRate)
                    .SetProperty(x => x.FeedCost, req.FeedCost)
                    .SetProperty(x => x.TotalSoldBirds, req.TotalSoldBirds)
                    .SetProperty(x => x.TotalBirdsWeight, req.TotalBirdsWeight)
                    .SetProperty(x => x.AvgBodyWeight, req.AvgBodyWeight)
                    .SetProperty(x => x.ChicksCost, req.ChicksCost)
                    .SetProperty(x => x.MedicineCost, req.MedicineCost)
                    .SetProperty(x => x.AdminCost, req.AdminCost)
                    .SetProperty(x => x.GrossProductionCost, req.GrossProductionCost)
                    .SetProperty(x => x.NetCostPerKg, req.NetCostPerKg)
                    .SetProperty(x => x.StdCostPerKg, req.StdCostPerKg)
                    .SetProperty(x => x.RearingChargesStd, req.RearingChargesStd)
                    .SetProperty(x => x.ProductionCostIncentive, req.ProductionCostIncentive)
                    .SetProperty(x => x.RearingChargesPerBird, req.RearingChargesPerBird)
                    .SetProperty(x => x.TotalRearingCharges, req.TotalRearingCharges)
                    .SetProperty(x => x.TotalAmountPayable, req.TotalAmountPayable)
                );

            if (updated == 0)
                return NotFound(new { Message = "Record not found." });

            return Ok(new { Message = "Final report updated successfully." });
        }

        // ----------------------- SOFT DELETE -----------------------
        [HttpPatch("soft-delete/{id}")]
        public IActionResult SoftDelete(int id)
        {
            if (id <= 0)
                return BadRequest(new { Message = "Invalid ID" });

            var deleted = _dbContext.FinalReport
                .Where(x => x.Id == id && !x.IsDeleted)
                .ExecuteUpdate(setter => setter.SetProperty(x => x.IsDeleted, true));

            if (deleted == 0)
                return NotFound(new { Message = "Record not found or already deleted." });

            return Ok(new { Message = "Record soft deleted successfully." });
        }

        // ----------------------- DELETE -----------------------
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new { Message = "Invalid ID" });

            var deleted = _dbContext.FinalReport
                .Where(x => x.Id == id)
                .ExecuteDelete();

            if (deleted == 0)
                return NotFound(new { Message = "Record not found." });

            return Ok(new { Message = "Record permanently deleted successfully." });
        }
    }
}
