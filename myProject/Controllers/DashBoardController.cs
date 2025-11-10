using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.AppDbContextNameSpace;

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
        public IActionResult Get()
        {
            var latestBatch = _DbContext.BirdInventory
                .Where(x => x.IsDeleted == false && x.Status == Models.BatchStatus.Ongoing)
                .OrderByDescending(x => x.Id)
                .Select(x => new { x.BatchNo, x.HousedBirdCount })
                .FirstOrDefault();

            if (latestBatch == null)
            {
                return NotFound(new { Message = "No active batch found." });
            }

            var TotalMortality = _DbContext.DailyRecord.Where(x=>x.BatchNo== latestBatch.BatchNo).Sum(x=>(int?)x.MortalityCount)??0;

            var TotalAliveBirds = latestBatch?.HousedBirdCount - TotalMortality;

            var TotalFeedConsume = _DbContext.DailyRecord.Where(x => x.BatchNo == latestBatch.BatchNo).Sum(x => (int?)x.FeedConsumedBags) ?? 0;

            var TotalFeedBags = _DbContext.FeedInventory.Where(x => x.BatchNo == latestBatch.BatchNo).Sum(x=> (int?)x.BagsArrivedCount) ?? 0;

            var TotalFeedBagsAvailable = TotalFeedBags - TotalFeedConsume;

            return Ok(new { Message = "Dashboard fetched successfully", Data =new { TotalMortality, TotalAliveBirds, TotalFeedConsume, TotalFeedBags, TotalFeedBagsAvailable } });
        }
    }
}
