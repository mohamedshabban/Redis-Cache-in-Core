using Microsoft.AspNetCore.Mvc;
using Redis.Api.Services;

namespace Redis.Api.Controller
{
    [Route("api/cache")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private readonly ICacheService _cacheService;

        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        [HttpGet(template: "{key}")]
        public async Task<IActionResult> GetCache(string key)
        {
            try
            {
                string value = await _cacheService.GetCacheValueAsync(key);
                return String.IsNullOrWhiteSpace(value) ? NotFound() : Ok(value);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost(template: "set/{key}/{value}")]
        public async Task<IActionResult> SetCache(string key, string value)
        {
            try
            {
                await _cacheService.SetCacheValueAsync(key, value);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
