using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myProject.dbContextNameSpace;
using myProject.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace myProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdsInventoryController : ControllerBase
    {
        private readonly dbContext _dbContext;

        public BirdsInventoryController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var inventoryList = _dbContext.BirdInventory.ToList();
            return Ok(inventoryList);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BirdInventory birdInventory)
        {
            var inventory = new BirdInventory { 
                Date=birdInventory.Date,
                NumberOfBox= birdInventory.NumberOfBox, 
                NumberOfBirds= birdInventory.NumberOfBirds,
                Total = birdInventory.Total,
                NumberOfBirdsArrived = birdInventory.NumberOfBirdsArrived,
                NumberOfBoxMortality= birdInventory.NumberOfBoxMortality,
                NumberOfWeaks= birdInventory.NumberOfWeaks,
                NumberOfExcess = birdInventory.NumberOfExcess,
                NumberOfBirdsHoused= birdInventory.NumberOfBirdsHoused
            };

            _dbContext.Add(inventory);
            _dbContext.SaveChanges();

            return Ok(inventory);
        }
    }
}
