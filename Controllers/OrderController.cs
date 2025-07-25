using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Services.Ord;
using PetGrubBakcend.Services.Razorpay;
using PetGrubBakcend.ApiResponse;

namespace PetGrubBakcend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService service;
        private readonly IRazorPayService _razorPayService;
        public OrderController(IOrderService orderService,IRazorPayService razorService)
        {
            service = orderService;
            _razorPayService = razorService;
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

        [Authorize(Policy ="UserOnly")]
        [HttpPost("BulkCartOrder")]
        public async Task<IActionResult> PostBulkOrder(int addressId)
        {
            var userIdStr = HttpContext.Items["UserId"]?.ToString();
            bool id = int.TryParse(userIdStr, out int userId);

            var res = await service.BulkOrderFromCart(userId,addressId);
            return StatusCode(res.StatusCode, res);
        }

        [Authorize]
        [HttpGet("GetOrderDetails")]
        public async Task<IActionResult> GetOrders()
        {
            var userIdStr = HttpContext.Items["UserId"]?.ToString();
            bool id = int.TryParse(userIdStr, out int userId);

            var res = await service.GetOrders(userId);
            return StatusCode(res.StatusCode, res);
        }

        [Authorize(Policy ="AdminOnly")]
        [HttpGet("WholeOrdersForAdmin")]
        public async Task<IActionResult> GetWhole()
        {
            var res = await service.GetOrdersWhole();
            return StatusCode(res.StatusCode, res);
        }


        //razorpay
        [Authorize(Policy ="UserOnly")]
        [HttpPost("Razor-CreateOrder")]
        public async Task<IActionResult> CreateOrder(long price)
        {
            try
            {
                if (price < 0)
                    return BadRequest(new ApiResponse<string> { Message = "enter a valid amount" });

                //call service to create razorpay order
                var orderId = await _razorPayService.CreatePaymentAsync(price);

                //return order id in response
                return Ok(new ApiResponse<string> { Message = "Created Order" });
            }
            catch(Exception ex)
            {
                return BadRequest(new ApiResponse<object> { Message = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("Razor-PaymentVerify")]
        public async Task<IActionResult> RazorPaymentVerify([FromBody] PaymentDto razorPay)
        {
            try
            {
                if(razorPay == null || 
                    string.IsNullOrEmpty(razorPay.razorpay_payment_id) ||
                    string.IsNullOrEmpty(razorPay.razorpay_order_id) ||
                    string.IsNullOrEmpty(razorPay.razorpay_signature))
                {
                    return BadRequest(new ApiResponse<string> { Message = "invalid razorpay payment details" });
                }

                //verify payment via service
                var res = await _razorPayService.verifyPaymentSignatureAsync(razorPay);

                if (!res)
                {
                    return BadRequest(new ApiResponse<object> { Message = "Error payment verification" });
                }

                return Ok(new ApiResponse<object> { Message = "Payment verified" });
            }
            catch(Exception ex)
            {
                return BadRequest(new ApiResponse<object> { Message = $"Error verifyig payment : {ex.Message}" });
            }
        }
    }
}
