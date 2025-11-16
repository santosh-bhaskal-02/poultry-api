using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.AppDbContextNameSpace;
using MyProject.Models;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        public readonly AppDbContext _DbContext;


        public DashBoardController(AppDbContext dbContext) 
        {
            _DbContext = dbContext;
        }

        [HttpGet]
 
        public IActionResult GetDashboard()
        {
            var latestBatch = _DbContext.Batch
                .Where(x => x.IsDeleted == false && x.Status == BatchStatus.Ongoing)
                .FirstOrDefault();

            if (latestBatch == null)
                return NotFound(new { Message = "No active batch found." });

            int batchId = latestBatch.Id;

            var birdInventory = _DbContext.BirdInventory
                .FirstOrDefault(x => x.BatchId == batchId && x.IsDeleted == false);

            if (birdInventory == null)
                return BadRequest(new { Message = "Bird arrival not yet recorded for this batch." });

            int housedBirds = birdInventory.HousedBirdCount;

            int totalMortality = _DbContext.DailyRecord
                .Where(x => x.BatchId == batchId && x.IsDeleted == false)
                .Sum(x => (int?)x.MortalityCount) ?? 0;

            int totalAliveBirds = housedBirds - totalMortality;

            int totalFeedConsume = _DbContext.DailyRecord
                .Where(x => x.BatchId == batchId && x.IsDeleted == false)
                .Sum(x => (int?)x.FeedConsumedBags) ?? 0;

            int totalFeedBags = _DbContext.FeedInventory
                .Where(x => x.BatchId == batchId && x.IsDeleted == false)
                .Sum(x => (int?)x.BagsArrivedCount) ?? 0;

            int totalFeedBagsAvailable = totalFeedBags - totalFeedConsume;

            return Ok(new
            {
                Message = "Dashboard fetched successfully",
                Data = new
                {
                    TotalMortality = totalMortality,
                    TotalAliveBirds = totalAliveBirds,
                    TotalFeedConsume = totalFeedConsume,
                    TotalFeedBags = totalFeedBags,
                    TotalFeedBagsAvailable = totalFeedBagsAvailable
                }
            });
        }

    }
}
