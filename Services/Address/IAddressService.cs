using PetGrubBakcend.DTOs;
using PetGrubBakcend.ApiResponse;

namespace PetGrubBakcend.Services.Address
{
    public interface IAddressService
    {
        Task<ApiResponse<AddressReadDto>> CreateAddress(AddressCreateDto addressCreateDto,int userId);

        Task<ApiResponse< List<AddressReadDto>>> GetAddresses();
        Task<ApiResponse<object>> DeleteAddress(int addressId, int userId);
    }
}
