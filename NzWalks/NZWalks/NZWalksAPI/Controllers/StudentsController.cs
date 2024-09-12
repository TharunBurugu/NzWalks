using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace NZWalksAPI.Controllers
{
    // https localhost:portnumber/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // Get localhost:portnumber/api/students
        [HttpGet]
        public IActionResult GetAllstudents()
        {
            string[] studentname = new string[] {"tharun","nani", "Rath", "Richads"};
            return Ok(studentname);
        }
    }
}
