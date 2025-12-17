using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.AppDbContextNameSpace;
using MyProject.DTOs.FeedInventory;
using MyProject.Models;
using MyProject.Utilities;

namespace MyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedInventoryController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public FeedInventoryController(ILogger<FeedInventoryController> logger, AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var batchId = _dbContext.Batch
              .Where(x => x.IsDeleted == false && x.Status == BatchStatus.Ongoing)
              .Select(x => x.Id)
              .FirstOrDefault();

            var inventory = _dbContext.FeedInventory
                .Where(x => x.IsDeleted == false && x.BatchId == batchId)
                .OrderByDescending(x => x.Date)
                .ToList();

            return Ok(new { Message = "Feed inventories fetched successfully", data = inventory });
        }

        [HttpPost]
        public IActionResult Create([FromBody] FeedInventoryRequest feedInventory)
        {
            Console.WriteLine("bags", feedInventory.BagsArrivedCount);
            if (feedInventory == null)
                return BadRequest(new { Message = "Invalid request data." });

            var batchId = _dbContext.Batch
                .Where(x => x.IsDeleted == false && x.Status == BatchStatus.Ongoing && x.IsDeleted==false)     
                .Select(x => x.Id)
                .FirstOrDefault();

            if (batchId == 0)
                return BadRequest(new { Message = "No active batch found." });

            feedInventory.Date = DateTimeHelper.NormalizeToUtc(feedInventory.Date);


            var record = new FeedInventory
            {
                BatchId = batchId,
                Date = feedInventory.Date,
                FeedName = feedInventory.FeedName,
                BagsArrivedCount = feedInventory.BagsArrivedCount,
                DriverName = feedInventory.DriverName,
                DriverPhoneNumber = feedInventory.DriverPhoneNumber,
                IsDeleted = false
            };

            _dbContext.FeedInventory.Add(record);
            _dbContext.SaveChanges();



            return Ok(new
            {
                Message = "Feed inventory record added successfully.",
                Record = record
            });
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] FeedInventory feedInventory)
        {
            if (id <= 0)
                return BadRequest(new { Message = "Invalid ID" });

            feedInventory.Date = DateTimeHelper.NormalizeToUtc(feedInventory.Date);


            var updatedCount = _dbContext.FeedInventory
                .Where(x => x.Id == id && x.IsDeleted == false)
                .ExecuteUpdate(setter => setter
                    .SetProperty(x => x.Date, feedInventory.Date)
                    .SetProperty(x => x.FeedName, feedInventory.FeedName)
                    .SetProperty(x => x.BagsArrivedCount, feedInventory.BagsArrivedCount)
                    .SetProperty(x => x.DriverName, feedInventory.DriverName)
                    .SetProperty(x => x.DriverPhoneNumber, feedInventory.DriverPhoneNumber)
                );

            if (updatedCount == 0)
                return NotFound(new { Message = "Record not found or already deleted." });

            return Ok(new { Message = "Feed inventory record updated successfully." });
        }

        [HttpPatch("soft-delete/{id}")]
        public IActionResult SoftDelete([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest(new { Message = "Invalid ID" });

            var updatedCount = _dbContext.FeedInventory
                .Where(x => x.Id == id && x.IsDeleted == false)
                .ExecuteUpdate(setter => setter.SetProperty(x => x.IsDeleted, true));

            if (updatedCount == 0)
                return NotFound(new { Message = "Record not found or already deleted." });

            return Ok(new
            {
                Message = "Record soft-deleted successfully.",
                UpdatedRecords = updatedCount
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest(new { Message = "Invalid ID" });

            var deletedCount = _dbContext.FeedInventory
                .Where(x => x.Id == id)
                .ExecuteDelete();

            if (deletedCount == 0)
                return NotFound(new { Message = "Record not found." });

            return Ok(new
            {
                Message = "Record permanently deleted successfully.",
                DeletedRecords = deletedCount
            });
        }
    }
}
