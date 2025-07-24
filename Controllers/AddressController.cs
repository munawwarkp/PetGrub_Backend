using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Services.Address;

namespace PetGrubBakcend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService service)
        {
            _addressService = service;
        }

        [HttpPost("Add-Address")]
        public async Task<IActionResult> AddAddress(AddressCreateDto addressCreateDto)
        {
            var userIdStr = HttpContext.Items["UserId"]?.ToString();
            bool id = int.TryParse(userIdStr, out int userId);

           var res = await _addressService.CreateAddress(addressCreateDto,userId);
            return StatusCode(res.StatusCode, res);

        }

        [HttpGet("GetAddresses")]
        public async Task<IActionResult> GetAddresses()
        {
            var userIdstr = HttpContext.Items["UserId"]?.ToString();
            bool id = int.TryParse(userIdstr, out int userId);

            var res = await _addressService.GetAddresses();
            return StatusCode(res.StatusCode, res);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleletAddress(int addressId)
        {
            var userIdStr = HttpContext.Items["UserId"]?.ToString();
            bool id = int.TryParse(userIdStr,out int userId);

            var res = await _addressService.DeleteAddress(addressId,userId);
            return StatusCode(res.StatusCode, res);
        }
    }
}
