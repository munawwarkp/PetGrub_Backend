using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Services.Ord;

namespace PetGrubBakcend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService service;
        public OrderController(IOrderService orderService)
        {
            service = orderService;
        }

        [Authorize(Policy = "UserOnly")]
        [HttpPost("SingleProductOrder")]
        public async Task<IActionResult> CreateSingleOrder(int productId,int addressId)
        {
           var userIdStr = HttpContext.Items["UserId"]?.ToString();
            bool id = int.TryParse(userIdStr, out int userId);

            var res = await service.CreateOrderProductSingle(userId,productId,addressId);
            return StatusCode(res.StatusCode, res);
        }
    }
}
