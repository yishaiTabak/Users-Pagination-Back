using Microsoft.AspNetCore.Mvc;
using ZigitTest.Models;
using ZigitTest.Repositories;


namespace ZigitTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepository _usersRepository;

        public UsersController(IUserRepository userRepository)
        {
            _usersRepository = userRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int skip, [FromQuery] int take)
        {
            try
            {
                if(skip<0 || take <= 0)
                {
                    return BadRequest("invalid filters");
                }
                List<UserModel> users = await _usersRepository.getUsers(skip, take);
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Catch any other unhandled exceptions
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpGet("user-count")]
        public async Task<IActionResult> GetUsersCount()
        {
            try
            {
                int count = await _usersRepository.CountUsers();
                return Ok(count);
            }
            catch (Exception ex)
            {
                // Catch any other unhandled exceptions
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpPost("insert-random/{count}")]
        public async Task<IActionResult> InsertRandomUsers([FromRoute] int count)
        {
            try
            {
                if (count <= 0)
                {
                    return BadRequest("The count must be greater than 0.");
                }
                await _usersRepository.InsertFakeUsers(count);
                return StatusCode(201);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
