using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.AppDbContextNameSpace;
using MyProject.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

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


        [HttpPost]
        public IActionResult Create([FromBody] BirdInventory birdInventory)
        {

            Console.WriteLine("birdInventory" + birdInventory.BatchNo);
            if (birdInventory == null)
                return BadRequest(new { Message = "Invalid payload" });

            var newInventory = new BirdInventory
            {
                Date = birdInventory.Date,
                BatchNo = birdInventory.BatchNo,
                BoxCount = birdInventory.BoxCount,
                BirdsPerBoxCount = birdInventory.BirdsPerBoxCount,
                TotalBirdCount = birdInventory.TotalBirdCount,
                BirdsArrivedCount = birdInventory.BirdsArrivedCount,
                BoxMortalityCount = birdInventory.BoxMortalityCount,
                DisabledBirdCount = birdInventory.DisabledBirdCount,
                WeakBirdCount = birdInventory.WeakBirdCount,
                ExcessBirdCount = birdInventory.ExcessBirdCount,
                HousedBirdCount = birdInventory.HousedBirdCount,
                Status = birdInventory.Status
            };

            _dbContext.BirdInventory.Add(newInventory);
            _dbContext.SaveChanges();

            return Ok(new
            {
                Message = "Record created successfully",
                Data = newInventory
            });
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] BirdInventory birdInventory)
        {
            var updatedRows = _dbContext.BirdInventory
                .Where(x => x.Id == id && x.IsDeleted == false)
                .ExecuteUpdate(setters => setters
                    .SetProperty(x => x.Date, birdInventory.Date)
                    .SetProperty(x => x.BatchNo, birdInventory.BatchNo)
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
