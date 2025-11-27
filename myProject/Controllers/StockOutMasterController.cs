using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.AppDbContextNameSpace;
using MyProject.DTOs.StockOut;
using MyProject.Models;
using MyProject.Utilities;

namespace MyProject.Controllers
{
    [ApiController]
    [Route("api/stockout/master")]
    public class StockOutMasterController : ControllerBase
    {
        private readonly AppDbContext _db;

        public StockOutMasterController(AppDbContext db)
        {
            _db = db;
        }

        // CREATE MASTER
        [HttpPost]
        public IActionResult CreateMaster()
        {
            // Find active batch
            var batchId = _db.Batch
                             .Where(x => x.IsDeleted == false && x.Status == BatchStatus.Ongoing)
                             .Select(x => x.Id)
                             .FirstOrDefault();

            if (batchId == 0)
                return BadRequest(new { Message = "No active batch found." });

            // Auto-increment StockOutNo
            int nextStockOutNo = _db.StockOutMaster
                                    .OrderByDescending(x => x.StockOutNo)
                                    .Select(x => x.StockOutNo)
                                    .FirstOrDefault() + 1;

            // Create Master record
            var master = new StockOutMaster
            {
                Date = DateTime.UtcNow,
                BatchId = batchId,
                StockOutNo = nextStockOutNo,
                TotalBirds = 0,
                TotalWeight = 0,
                AvgWeight = 0
            };

            _db.StockOutMaster.Add(master);
            _db.SaveChanges();

            return Ok(new { message = "Master created!", masterId = master.Id, stockOutNo = master.StockOutNo });
        }


        // FINALIZE SUMMARY
        [HttpPut("finalize/{id}")]
        public IActionResult FinalizeMaster(int id, [FromBody] StockOutMasterFinalizeDto dto)
        {
            var master = _db.StockOutMaster.Find(id);
            if (master == null)
                return NotFound("Master record not found.");

            master.TotalBirds = dto.TotalBirds;
            master.TotalWeight = dto.TotalWeight;
            master.AvgWeight = dto.AvgWeight;

            _db.SaveChanges();

            return Ok("Master finalized!");
        }
    }
}
