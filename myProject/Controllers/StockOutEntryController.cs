using Microsoft.AspNetCore.Mvc;
using MyProject.AppDbContextNameSpace;
using MyProject.DTOs.StockOut;

using MyProject.Models;

namespace MyProject.Controllers
{
    [ApiController]
    [Route("api/stockout/entry")]
    public class StockOutEntryController : ControllerBase
    {
        private readonly AppDbContext _db;

        public StockOutEntryController(AppDbContext db)
        {
            _db = db;
        }

        // ADD ENTRY
        [HttpPost]
        public IActionResult AddEntry([FromBody] StockOutEntryCreateDto dto)
        {
            int lastSr = 0;

            if (_db.StockOutEntries.Any(x => x.StockOutMasterId == dto.StockOutMasterId))
            {
                lastSr = _db.StockOutEntries
                    .Where(x => x.StockOutMasterId == dto.StockOutMasterId)
                    .Max(x => x.SrNo);
            }

            var entry = new StockOutEntry
            {
                StockOutMasterId = dto.StockOutMasterId,
                Birds = dto.Birds,
                Weight = dto.Weight,
                SrNo = lastSr + 1
            };

            _db.StockOutEntries.Add(entry);
            _db.SaveChanges();

            return Ok(new { message = "Entry added!", entry });
        }


        // GET ENTRIES
        [HttpGet("{masterId}")]
        public IActionResult GetEntries(int masterId)
        {
            var entries = _db.StockOutEntries
                .Where(e => e.StockOutMasterId == masterId)
                .OrderBy(e => e.SrNo)
                .ToList();

            return Ok(new {message ="Previous Entries Fetched Successfully", entries });
        }


        //[HttpPut("{id}")]
        [HttpPut("{id}")]
        public IActionResult EditEntry(int id, [FromBody] UpdateStockOutEntryRequest dto)
        {
            Console.WriteLine("id", id);
            if (dto == null)
                return BadRequest(new { message = "Invalid request body" });

            var entry = _db.StockOutEntries.FirstOrDefault(x => x.Id == id);

            if (entry == null)
                return NotFound(new { message = "Entry not found" });

            entry.SrNo = dto.SrNo;
            entry.Weight = dto.Weight;
            entry.Birds = dto.Birds;
            entry.Weight = dto.Weight;

            _db.StockOutEntries.Update(entry);
            _db.SaveChanges();

            return Ok(new
            {
                message = "Entry updated successfully",
                updatedEntry = entry
            });
        }


        // DELETE ENTRY
        [HttpDelete("{id}")]
        public IActionResult DeleteEntry(int id)
        {
            Console.WriteLine("id"+ id);
            var entry = _db.StockOutEntries.Find(id);
            if (entry == null)
                return NotFound();

            _db.StockOutEntries.Remove(entry);
            _db.SaveChanges();

            return Ok("Entry deleted!");
        }
    }
}
