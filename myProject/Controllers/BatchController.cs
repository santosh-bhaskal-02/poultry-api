using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.AppDbContextNameSpace;
using MyProject.Models;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly AppDbContext _db;

        public BatchController(AppDbContext db) => _db = db;

        // CREATE NEW BATCH
        [HttpPost]
        public IActionResult Create([FromBody] Batch batch)
        {
            bool exists = _db.Batch.Any(b => b.Status == BatchStatus.Ongoing);

            if (exists)
                return BadRequest("Another batch is already active. Close it before adding a new batch.");

            batch.StartDate = DateTime.UtcNow;
            batch.Status = BatchStatus.Ongoing;

            _db.Batch.Add(batch);
            _db.SaveChanges();

            return Ok(new { message = "Batch created", data = batch });
        }

        // GET ACTIVE BATCH
        [HttpGet("active")]
        public IActionResult GetActiveBatch()
        {
            var batch = _db.Batch.FirstOrDefault(x => x.Status == BatchStatus.Ongoing);
            if (batch == null)
                return NotFound("No active batch found.");

            return Ok(batch);
        }

        // CLOSE BATCH
        [HttpPatch("close/{batchId}")]
        public IActionResult CloseBatch(int batchId)
        {
            var batch = _db.Batch.Find(batchId);
            if (batch == null)
                return NotFound("Batch not found.");

            if (batch.Status == BatchStatus.Completed)
                return BadRequest("This batch is already closed.");

            batch.Status = BatchStatus.Completed;
            batch.EndDate = DateTime.UtcNow;

            _db.SaveChanges();

            return Ok("Batch closed successfully.");
        }

        // GET ALL BATCHES
        [HttpGet]
        public IActionResult GetAll()
        {
            var batches = _db.Batch
                .Include(b => b.BirdInventory)
                .Include(b => b.FeedInventories)
                .Include(b => b.DailyRecords)
                .ToList();

            return Ok(batches);
        }
    }
}
