using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using myProject.dbContextNameSpace;
using myProject.Models;


namespace myProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class DailyRegisterController : ControllerBase
    {
        private readonly dbContext _dbContext;

        public DailyRegisterController(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _dbContext.DailyRegister.ToList().OrderByDescending(x=>x.Date);  
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] DailyRegister dailyRegister)
        {
            Console.WriteLine("data", dailyRegister.Date);
            var report = new DailyRegister { Date = dailyRegister.Date,Age= dailyRegister.Age, DailyFeedConsume = dailyRegister.DailyFeedConsume, DailyMortality = dailyRegister.DailyMortality };
          
            _dbContext.DailyRegister.Add(report);
            _dbContext.SaveChanges();
            return Ok(report); 
        }
    }
}
