using InfotecsTestWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InfotecsTestWebApi.Controllers
{
    [ApiController]
    [Route("api/values")]
    public class ValuesController : ControllerBase
    {
        private readonly ValuesService _valuesService;
        public ValuesController(ValuesService valuesService)
        {
            _valuesService = valuesService;
        }
        [HttpGet("last")]
        public IActionResult GetLastValues([FromQuery] string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return BadRequest("Filename is required");
            var values = _valuesService.GetValuesByFile(fileName);
            return Ok(values);
        }
    }
}
