using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetGrubBakcend.Services.wishlist;
using PetGrubBakcend.ApiResponse;

using PetGrubBakcend.DTOs;
using Microsoft.AspNetCore.Authorization;
using PetGrubBakcend.Entities;
using System.Security.Claims;

namespace PetGrubBakcend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;
        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [Authorize(Policy = "UserOnly")]
        [HttpPost]
        public async Task<IActionResult> AddWishlist(int productId)
        {
              var stringUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                bool id = int.TryParse(stringUserId, out int userId);
            if (!id)
            {
                return BadRequest(new { message = "Invalid or missing user id" });
            }
            var res = await _wishlistService.AddWishlistIfNotExist(userId,productId);
            return StatusCode(res.StatusCode,res);
        }

        [Authorize("UserOnly")]
        [HttpGet("Wishlist")]
        public async Task<IActionResult> Get()
        {

            //var userIdClaim = User.FindFirst(ClaimTypes. NameIdentifier)?.Value;

            //if (string.IsNullOrEmpty(userIdClaim))
            //    return Unauthorized("User id not found in token");

            //int userId = int.Parse(userIdClaim);

            var userIdString = HttpContext.Items["UserId"]?.ToString();
            bool id = int.TryParse(userIdString, out int userId);

            if (id)
            {
                var res = await _wishlistService.GetWishlistedProduct(userId);
                return StatusCode(res.StatusCode, res);
            }
            else
            {
                return Unauthorized("User id is missing or invalid");
            }

        }

        [Authorize("UserOnly")]
        [HttpDelete("RemoveFromWishlist/{productId}")]
        public async Task<IActionResult> Remove(int productId)
        {
           var stringUserId =  HttpContext.Items["UserId"]?.ToString();
            bool id = int.TryParse(stringUserId, out int userId);

            var res = await _wishlistService.RemoveWishlistedServ(userId, productId);
            return StatusCode(res.StatusCode, res);


        }
    }
}
