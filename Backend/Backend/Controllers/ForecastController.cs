using Backend.DTOs;
using Backend.ML_Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Backend.Controllers
{
    /// <summary>
    /// Controller for managing forecast files related to country and genre spending predictions.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ForecastFilesController : ControllerBase
    {
        /// <summary>
        /// Retrieves the forecast data for top country spending from a pre-generated CSV files.
        /// </summary>
        /// <returns>A prediction list of <see cref="ForecastCountryDto"/> objects.</returns>
        [HttpGet("country")]
        public async Task<IActionResult> GetCountryForecast()
        {
            try
            {
                var filePath = ForecastModel.ForecastTopCountrySpending();
                var json = await System.IO.File.ReadAllTextAsync(filePath);
                var forecastList = JsonSerializer.Deserialize<List<ForecastCountryDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (forecastList == null)
                    return Problem("Failed to deserialize forecast data.");

                return Ok(forecastList);
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
        /// <summary>
        /// Retrieves the forecast data for top genre spending from a pre-generated CSV files.
        /// </summary>
        /// <returns>A prediction list of <see cref="ForecastGenreDto"/> objects.</returns>
        [HttpGet("genre")]
        public async Task<IActionResult> GetGenreForecast()
        {
            try
            {
                var filePath = ForecastModel.ForecastTopGenres();
                var json = await System.IO.File.ReadAllTextAsync(filePath);
                var forecastList = JsonSerializer.Deserialize<List<ForecastGenreDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (forecastList == null)
                    return Problem("Failed to deserialize forecast data.");

                return Ok(forecastList);
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
