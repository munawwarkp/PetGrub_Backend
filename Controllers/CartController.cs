using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PetGrubBakcend.DTOs;
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
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var userId = HttpContext.Items["UserId"]?.ToString(); //userId taken from httpcontext.items
            bool id = int.TryParse(userId, out int idUser);
            var res = await _service.GetCartItems(idUser);
            return StatusCode(res.StatusCode, res);
           
        }

        [HttpGet("CartItemsForAdmin")]
        [Authorize(Policy ="AdminOnly")]
        public async Task<IActionResult> GetCartUser([FromQuery]int userId)
        {
            var res = await _service.GetCartItems(userId);
            return StatusCode(res.StatusCode, res);
        }

        [HttpPost("AddToCart")]
        [Authorize]
        public async Task<IActionResult> AddToCart(int productId)
        {
           var userIdStr = HttpContext.Items["UserId"]?.ToString();
            bool uId = int.TryParse(userIdStr, out int userId);
           var res = await _service.AddToCart(productId,userId);
            return StatusCode(res.StatusCode,res);
        }

        [HttpDelete("Delete-CartItem")]
        public async Task<IActionResult> DeleteCart([FromQuery]int productId)
        {
            var userIdStr = HttpContext.Items["UserId"]?.ToString();
            bool uId = int.TryParse(userIdStr, out int userId);

            var res = await _service.RemoveFromCart(userId, productId);
            return StatusCode(res.StatusCode, res);
        }

        [HttpPatch("Changing-quantity")]
        public async Task<IActionResult> UpdateQuantity([FromBody] UpdateCartActionDto updateCartActionDto)
        {
            var idStr = HttpContext.Items["UserId"]?.ToString();
            var id = int.TryParse(idStr, out int idUser);
 
            var res = await _service.IncreaseOrDecreaseQuantity(updateCartActionDto,idUser);
            return StatusCode(res.StatusCode, res);
        }

        [HttpDelete("Clear-cart")]
        public async Task<IActionResult> Clear()
        {
          var strId =  HttpContext.Items["UserId"]?.ToString();
            bool idInt = int.TryParse(strId,out int userId);

           var res = await _service.ClearCart(userId);
            return StatusCode(res.StatusCode, res); 

        }
    }
}
