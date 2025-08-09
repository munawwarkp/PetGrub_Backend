using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetGrubBakcend.Services.Usr;

namespace PetGrubBakcend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [Authorize(Policy="AdminOnly")]
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var res = await _service.GetUsers();
            return StatusCode(res.StatusCode, res);
        }

        [Authorize(Policy ="AdminOnly")]
        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUser([FromRoute]int id)
        {
            var res = await _service.GetUserById(id);
            return StatusCode(res.StatusCode, res);
        }

        [Authorize(Policy ="UserOnly")]
        [HttpGet("GetUserForUser")]
        public async Task <IActionResult> GetUserForUser()
        {
            var userIdStr = HttpContext.Items["UserId"]?.ToString();
            bool id = int.TryParse(userIdStr, out int userId);

            var res = await _service.GetUserById(userId);
            return StatusCode(res.StatusCode, res);
        }

        [HttpPatch("Block/Unblock/{id}")]
        [Authorize(Policy ="AdminOnly")]
        public async Task<IActionResult> Block(int id)
        {
            var res = await _service.BolckOUnblockUserService(id);
            return StatusCode(res.StatusCode, res);
        }
    }

}
