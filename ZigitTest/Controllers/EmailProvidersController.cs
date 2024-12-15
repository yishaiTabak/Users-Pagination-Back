using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZigitTest.Repositories;

namespace ZigitTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailProvidersController : ControllerBase
    {
        private readonly IEmailRepository _emailRepository;

        public EmailProvidersController(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProviders()
        {
            try
            {
                List<string> providers = await _emailRepository.GetProviders();
                if (providers == null || providers.Count() == 0)
                {
                    return NotFound();
                }

                return Ok(providers);

            } catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
