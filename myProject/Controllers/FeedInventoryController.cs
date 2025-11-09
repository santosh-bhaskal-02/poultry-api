using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.AppDbContextNameSpace;
using MyProject.Models;

namespace MyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedInventoryController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<FeedInventoryController> _logger;

        public FeedInventoryController(ILogger<FeedInventoryController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var batchNo = _dbContext.BirdInventory
              .Where(x => x.IsDeleted == false && x.Status == BatchStatus.Ongoing)
              .OrderByDescending(x => x.Id)
              .Select(x => x.BatchNo)
              .FirstOrDefault();

            var inventory = _dbContext.FeedInventory
                .Where(x => x.IsDeleted == false && x.BatchNo == batchNo)
                .OrderByDescending(x => x.Date)
                .ToList();

            return Ok(new { Message = "Feed inventories fetched successfully", data = inventory });
        }

        [HttpPost]
        public IActionResult Create([FromBody] FeedInventory feedInventory)
        {
            Console.WriteLine("bags", feedInventory.BagsArrivedCount);
            if (feedInventory == null)
                return BadRequest(new { Message = "Invalid request data." });

            var batchNo = _dbContext.BirdInventory
                .Where(x => x.IsDeleted == false && x.Status == BatchStatus.Ongoing)
                .OrderByDescending(x => x.Id)
                .Select(x => x.BatchNo)
                .FirstOrDefault();

            if (batchNo == 0)
                return BadRequest(new { Message = "No active batch found." });

            DateTime dateUtc;

            if (feedInventory.Date.Kind == DateTimeKind.Unspecified)
                dateUtc = DateTime.SpecifyKind(feedInventory.Date, DateTimeKind.Utc);
            else if (feedInventory.Date.Kind == DateTimeKind.Local)
                dateUtc = feedInventory.Date.ToUniversalTime();
            else
                dateUtc = feedInventory.Date;

            var record = new FeedInventory
            {
                BatchNo = batchNo,
                Date = dateUtc,
                FeedName = feedInventory.FeedName,
                BagsArrivedCount = feedInventory.BagsArrivedCount,
                DriverName = feedInventory.DriverName,
                DriverPhoneNumber = feedInventory.DriverPhoneNumber,
                IsDeleted = false
            };

            _dbContext.FeedInventory.Add(record);
            _dbContext.SaveChanges();

            _logger.LogInformation("Feed inventory record created successfully for batch {BatchId}", batchNo);

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

            DateTime dateUtc;

            if (feedInventory.Date.Kind == DateTimeKind.Unspecified)
                dateUtc = DateTime.SpecifyKind(feedInventory.Date, DateTimeKind.Utc);
            else if (feedInventory.Date.Kind == DateTimeKind.Local)
                dateUtc = feedInventory.Date.ToUniversalTime();
            else
                dateUtc = feedInventory.Date;

            var updatedCount = _dbContext.FeedInventory
                .Where(x => x.Id == id && x.IsDeleted == false)
                .ExecuteUpdate(setter => setter
                    .SetProperty(x => x.Date, dateUtc)
                    .SetProperty(x => x.FeedName, feedInventory.FeedName)
                    .SetProperty(x => x.BagsArrivedCount, feedInventory.BagsArrivedCount)
                    .SetProperty(x => x.DriverName, feedInventory.DriverName)
                    .SetProperty(x => x.DriverPhoneNumber, feedInventory.DriverPhoneNumber)
                );

            if (updatedCount == 0)
                return NotFound(new { Message = "Record not found or already deleted." });

            _logger.LogInformation("Feed inventory record with ID {Id} updated successfully", id);

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

            _logger.LogWarning("Feed inventory record with ID {Id} was soft-deleted", id);

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

            _logger.LogWarning("Feed inventory record with ID {Id} was permanently deleted", id);

            return Ok(new
            {
                Message = "Record permanently deleted successfully.",
                DeletedRecords = deletedCount
            });
        }
    }
}
