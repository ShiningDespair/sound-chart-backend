using Backend.ML_Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForecastFilesController : ControllerBase
    {
        [HttpGet("country")]
        public async Task<IActionResult> GetCountryForecast()
        {
            try
            {
                var filePath = ForecastModel.ForecastTopCountrySpending();
                var json = await System.IO.File.ReadAllTextAsync(filePath);
                return Content(json, "application/json");
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("genre")]
        public async Task<IActionResult> GetGenreForecast()
        {
            try
            {
                var filePath = ForecastModel.ForecastTopGenres();
                var json = await System.IO.File.ReadAllTextAsync(filePath);
                return Content(json, "application/json");
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
