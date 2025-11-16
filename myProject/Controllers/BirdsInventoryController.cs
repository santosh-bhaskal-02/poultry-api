using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.AppDbContextNameSpace;
using MyProject.DTOs.BirdInventory;
using MyProject.Models;
using MyProject.Utilities;


namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdsInventoryController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public BirdsInventoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var inventories = _dbContext.BirdInventory
                .Where(x => x.IsDeleted == false)
                .ToList();

            return Ok(new { Message = "Bird inventories fetched successfully", data = inventories });
        }

        [HttpGet("{id}")]
        public IActionResult GetByBatch(int id)
        {
            var inv = _dbContext.BirdInventory
                .FirstOrDefault(x => x.BatchId == id && x.IsDeleted == false);

            if (inv == null) return NotFound("No bird inventory found.");

            return Ok(new { Message = "Bird Inventory found successfully", Data = inv });
        }



        [HttpPost]
        public IActionResult Create([FromBody] BirdInventoryRequest birdInventory)
        {
            if (birdInventory == null )
                return BadRequest("Invalid request.");

        
            bool existingBatch = _dbContext.Batch.Any(b => b.Status == BatchStatus.Ongoing);
            if (existingBatch)
                return BadRequest("Another batch is already active. Close it before starting a new batch.");

            var batch = new Batch
            {
                BatchNo = birdInventory.BatchNo,
                StartDate = DateTime.UtcNow,
                Status = BatchStatus.Ongoing,
                IsDeleted = false
            };

            _dbContext.Batch.Add(batch);
            _dbContext.SaveChanges(); 

           
            birdInventory.Date = DateTimeHelper.NormalizeToUtc(birdInventory.Date);
            birdInventory.BatchId = batch.Id;


            var newInventory = new BirdInventory
            {
                Date = birdInventory.Date,
                BatchId = birdInventory.BatchId,
                BoxCount = birdInventory.BoxCount,
                BirdsPerBoxCount = birdInventory.BirdsPerBoxCount,
                TotalBirdCount = birdInventory.TotalBirdCount,
                BirdsArrivedCount = birdInventory.BirdsArrivedCount,
                BoxMortalityCount = birdInventory.BoxMortalityCount,
                DisabledBirdCount = birdInventory.DisabledBirdCount,
                WeakBirdCount = birdInventory.WeakBirdCount,
                ShortBirdCount = birdInventory.ShortBirdCount,
                ExcessBirdCount = birdInventory.ExcessBirdCount,
                HousedBirdCount = birdInventory.HousedBirdCount,
            };

            _dbContext.BirdInventory.Add(newInventory);
            _dbContext.SaveChanges();

            return Ok(new
            {
                Message = "Record created successfully",
                Data = new
                {
                    newInventory.Id,
                    newInventory.BatchId,
                    newInventory.Date,
                    newInventory.HousedBirdCount,
                    newInventory.TotalBirdCount
                }
            });
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] BirdInventory birdInventory)
        {
            birdInventory.Date = DateTimeHelper.NormalizeToUtc(birdInventory.Date);


            var updatedRows = _dbContext.BirdInventory
                .Where(x => x.Id == id && x.IsDeleted == false)
                .ExecuteUpdate(setters => setters
                    .SetProperty(x => x.Date, birdInventory.Date)
                    .SetProperty(x => x.BatchId, birdInventory.BatchId)
                    .SetProperty(x => x.BoxCount, birdInventory.BoxCount)
                    .SetProperty(x => x.BirdsPerBoxCount, birdInventory.BirdsPerBoxCount)
                    .SetProperty(x => x.TotalBirdCount, birdInventory.TotalBirdCount)
                    .SetProperty(x => x.BirdsArrivedCount, birdInventory.BirdsArrivedCount)
                    .SetProperty(x => x.BoxMortalityCount, birdInventory.BoxMortalityCount)
                    .SetProperty(x => x.DisabledBirdCount, birdInventory.DisabledBirdCount)
                    .SetProperty(x => x.WeakBirdCount, birdInventory.WeakBirdCount)
                    .SetProperty(x => x.ExcessBirdCount, birdInventory.ExcessBirdCount)
                    .SetProperty(x => x.HousedBirdCount, birdInventory.HousedBirdCount)
                );

            if (updatedRows == 0)
                return NotFound(new { Message = "Record not found or already deleted" });

            return Ok(new { Message = "Record updated successfully", UpdatedRows = updatedRows });
        }



        [HttpPatch("soft-delete/{id}")]
        public IActionResult SoftDelete([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest(new { Message = "Invalid ID" });

            var updatedCount = _dbContext.BirdInventory
                .Where(x => x.Id == id && x.IsDeleted == false)
                .ExecuteUpdate(setters => setters
                    .SetProperty(x => x.IsDeleted, true)
                );

            if (updatedCount == 0)
                return NotFound(new { Message = "Record not found or already deleted" });

            return Ok(new
            {
                Message = "Record soft-deleted successfully",
                UpdatedRecords = updatedCount
            });
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int id)
        {
            if (id <= 0)
                return BadRequest(new { Message = "Invalid ID" });

            var deletedCount = _dbContext.BirdInventory
                .Where(x => x.Id == id)
                .ExecuteDelete();

            if (deletedCount == 0)
                return NotFound(new { Message = "Record not found" });

            return Ok(new
            {
                Message = "Record permanently deleted successfully",
                DeletedRecords = deletedCount
            });
        }

    }
}
