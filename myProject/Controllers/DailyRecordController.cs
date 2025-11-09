using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject;
using MyProject.AppDbContextNameSpace;
using MyProject.Models;

namespace MyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DailyRecordController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public DailyRecordController(AppDbContext dbContext)
        {
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

            var records = _dbContext.DailyRecord
                .Where(x =>x.BatchNo==batchNo &&  x.IsDeleted == false)
                .OrderByDescending(x => x.Date)
                .ToList();

            return Ok(new {Message="Daily records fetched successfully", data = records });
        }

        [HttpPost]
        public IActionResult Create([FromBody] DailyRecord dailyRecord)
        {
            if (dailyRecord == null)
                return BadRequest(new { Message = "Invalid request data." });

            var batchNo = _dbContext.BirdInventory
                .Where(x => x.IsDeleted == false && x.Status == BatchStatus.Ongoing)
                .OrderByDescending(x => x.Id)
                .Select(x => x.BatchNo)
                .FirstOrDefault();

            if (batchNo == 0)
                return BadRequest(new { Message = "No active batch found." });

            var feedBalance = _dbContext.FeedInventory
                .Where(x => x.BatchNo == batchNo && x.IsDeleted == false)
                .Sum(x => x.BagsArrivedCount);

            var birdBalance = _dbContext.BirdInventory
                 .Where(x => x.BatchNo == batchNo && x.IsDeleted == false)
                 .Sum(x => x.HousedBirdCount);

            if (feedBalance < dailyRecord.FeedConsumedBags)
            {
                return BadRequest(new
                {
                    Message = "Not enough feed bags available.",
                    FeedBalance = feedBalance
                });
            }

            if (birdBalance < dailyRecord.MortalityCount)
            {
                return BadRequest(new
                {
                    Message = "Mortality count can't be greater than ttotal birds.",
                    birdBalance = birdBalance
                });
            }

            DateTime dateUtc;

            if (dailyRecord.Date.Kind == DateTimeKind.Unspecified)
                dateUtc = DateTime.SpecifyKind(dailyRecord.Date, DateTimeKind.Utc);
            else if (dailyRecord.Date.Kind == DateTimeKind.Local)
                dateUtc = dailyRecord.Date.ToUniversalTime();
            else
                dateUtc = dailyRecord.Date;

            var record = new DailyRecord
            {
                BatchNo = batchNo,
                Date = dateUtc,
                BirdAgeInDays = dailyRecord.BirdAgeInDays,
                FeedConsumedBags = dailyRecord.FeedConsumedBags,
                MortalityCount = dailyRecord.MortalityCount,
                IsDeleted = false
            };

            _dbContext.DailyRecord.Add(record);
            _dbContext.SaveChanges();

            return Ok(new
            {
                Message = "Daily record added successfully.",
                Record = record
            });
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] DailyRecord updatedRecord)
        {
            if (id <= 0)
                return BadRequest(new { Message = "Invalid ID" });

            DateTime dateUtc;

            if (updatedRecord.Date.Kind == DateTimeKind.Unspecified)
                dateUtc = DateTime.SpecifyKind(updatedRecord.Date, DateTimeKind.Utc);
            else if (updatedRecord.Date.Kind == DateTimeKind.Local)
                dateUtc = updatedRecord.Date.ToUniversalTime();
            else
                dateUtc = updatedRecord.Date;

            var updatedCount = _dbContext.DailyRecord
                .Where(x => x.Id == id && x.IsDeleted == false)
                .ExecuteUpdate(setter => setter
                    .SetProperty(x => x.Date, dateUtc)
                    .SetProperty(x => x.BirdAgeInDays, updatedRecord.BirdAgeInDays)
                    .SetProperty(x => x.FeedConsumedBags, updatedRecord.FeedConsumedBags)
                    .SetProperty(x => x.MortalityCount, updatedRecord.MortalityCount)
                );


            if (updatedCount == 0)
                return NotFound(new { Message = "Record not found or already deleted." });

            return Ok(new { Message = "Record updated successfully." });
        }

        [HttpPatch("soft-delete/{id}")]
        public IActionResult SoftDelete([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest(new { Message = "Invalid ID" });

            var updatedCount = _dbContext.DailyRecord
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

            var deletedCount = _dbContext.DailyRecord
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
