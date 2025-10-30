using Microsoft.AspNetCore.Mvc;
using myProject.dbContextNameSpace;
using myProject.Models;

namespace myProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedInventoryController : ControllerBase
    {

        private readonly dbContext _dbContext;
        private readonly ILogger<FeedInventoryController> _logger;
      
        public FeedInventoryController(ILogger<FeedInventoryController> logger, dbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;

        }

        [HttpGet]
        public IActionResult Get()
        {
            var inventory = _dbContext.FeedInventory.ToList();
            return Ok(inventory);
        }

        [HttpPost]
        public IActionResult Post([FromBody] FeedInventory feedInventory)
        {
            var inventory = new FeedInventory { 
                Date=feedInventory.Date,
                FeedName=feedInventory.FeedName,
                NumberOfBagsArrived=feedInventory.NumberOfBagsArrived,
                DriverName=feedInventory.DriverName,
                DriverPhoneNumber=feedInventory.DriverPhoneNumber
            };

            _dbContext.FeedInventory.Add(feedInventory);
            _dbContext.SaveChanges();
           
            return Ok(feedInventory);
        }
    }
}
