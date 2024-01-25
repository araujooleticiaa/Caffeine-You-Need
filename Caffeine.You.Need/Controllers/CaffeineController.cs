using Caffeine.You.Need.Interface;
using Caffeine.You.Need.Models;
using Microsoft.AspNetCore.Mvc;

namespace Caffeine.You.Need.Controllers;

[ApiController]
public class CaffeineController : ControllerBase
{
    private readonly ICaffeineRepository _caffeineRepository;
    public CaffeineController(ICaffeineRepository caffeineRepository)
    {
        _caffeineRepository = caffeineRepository;
    }

    [HttpPost("/v1/calculate")]
    public async Task<ActionResult<List<Recommendation>>> CreateUsuario(
        [FromBody] List<RecommendationRequest> recommendation)
    {
        try
        {
            var result = await _caffeineRepository.CreateRecommendations(recommendation);
            return Ok(result);
        }
        catch
        {
            return StatusCode(500, "Internal server failure.");
        }
    }

    [HttpGet("/v1/coffees")]
    public async Task<ActionResult<List<Coffee>>> GetUsuario()
    {
        try
        {
            var result = await _caffeineRepository.GetCoffees();
            return Ok(result);
        }
        catch
        {
            return StatusCode(500, "Internal server failure.");
        }
    }
}
