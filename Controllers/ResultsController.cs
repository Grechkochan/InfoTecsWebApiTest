using InfotecsTestWebApi.Models;
using InfotecsTestWebApi.Services;
using Microsoft.AspNetCore.Mvc;
namespace InfotecsTestWebApi.Controllers
{
    [ApiController]
    [Route("api/results")]
    public class ResultsController : ControllerBase
    {
        private readonly FilterService _filterService;
        public ResultsController(FilterService filterService)
        {
            _filterService = filterService;
        }
        [HttpGet]
        public IActionResult GetResults([FromQuery] FilterModel filter)
        {
            var results = _filterService.GetFilteredData(filter);
            return Ok(results);
        }
    }
}