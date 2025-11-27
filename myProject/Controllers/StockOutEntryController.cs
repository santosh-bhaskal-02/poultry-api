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

        // DELETE ENTRY
        [HttpDelete("{id}")]
        public IActionResult DeleteEntry(int id)
        {
            var entry = _db.StockOutEntries.Find(id);
            if (entry == null)
                return NotFound();

            _db.StockOutEntries.Remove(entry);
            _db.SaveChanges();

            return Ok("Entry deleted!");
        }
    }
}
