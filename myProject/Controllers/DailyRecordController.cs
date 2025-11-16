using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject;
using MyProject.AppDbContextNameSpace;
using MyProject.DTOs.DailyRecord;
using MyProject.Models;
using MyProject.Utilities;

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
            var batchId = _dbContext.Batch
             .Where(x => x.IsDeleted == false && x.Status == BatchStatus.Ongoing)
             .Select(x => x.Id)
             .FirstOrDefault();

            if (batchId == 0)
                return Ok(new { Message = "No active batch found.", Data = new List<DailyRecord>() });


            var records = _dbContext.DailyRecord
                 .AsNoTracking()
                .Where(x =>x.BatchId== batchId &&  x.IsDeleted == false)
                .OrderByDescending(x => x.Date)
                .ToList();

            return Ok(new {Message="Daily records fetched successfully", Data = records });
        }

        [HttpPost]
        public IActionResult Create([FromBody] DailyRecordRequest dailyRecord)
        {
            if (dailyRecord == null)
                return BadRequest(new { Message = "Invalid request data." });

            var batchId = _dbContext.Batch
                .Where(x => x.IsDeleted == false && x.Status == BatchStatus.Ongoing)
                .Select(x => x.Id)
                .FirstOrDefault();

            if (batchId == 0)
                return BadRequest(new { Message = "No active batch found." });

            var feedBalance = _dbContext.FeedInventory
                .Where(x => x.BatchId == batchId && x.IsDeleted == false)
                .Sum(x => x.BagsArrivedCount);

            var birdBalance = _dbContext.BirdInventory
                 .Where(x => x.BatchId == batchId && x.IsDeleted == false)
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

             dailyRecord.Date =  DateTimeHelper.NormalizeToUtc(dailyRecord.Date);

            var record = new DailyRecord
            {
                BatchId = batchId,
                Date = dailyRecord.Date,
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
        public IActionResult Update([FromRoute] int id, [FromBody] DailyRecordUpdateRequest updatedRecord)
        {
            if (id <= 0)
                return BadRequest(new { Message = "Invalid ID" });

            updatedRecord.Date = DateTimeHelper.NormalizeToUtc(updatedRecord.Date);

            var updatedCount = _dbContext.DailyRecord
                .Where(x => x.Id == id && x.IsDeleted == false)
                .ExecuteUpdate(setter => setter
                    .SetProperty(x => x.Date, updatedRecord.Date)
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
