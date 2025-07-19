using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetGrubBakcend.Services.Cart;

namespace PetGrubBakcend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;
        public CartController(ICartService service)
        {
            _service = service;
        }

        [HttpGet("CartItems")]
        public async Task<IActionResult> Get()
        {
            var userId = HttpContext.Items["UserId"]?.ToString(); //userId taken from httpcontext.items
            bool id = int.TryParse(userId, out int idUser);
            var res = await _service.GetCartItems(idUser);
            return StatusCode(res.StatusCode, res);
           
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(int productId)
        {
           var userIdStr = HttpContext.Items["UserId"]?.ToString();
            bool uId = int.TryParse(userIdStr, out int userId);
           var res = await _service.AddToCart(productId,userId);
            return StatusCode(res.StatusCode,res);
        }
    }
}
