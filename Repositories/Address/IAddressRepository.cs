using PetGrubBakcend.DTOs;
using PetGrubBakcend.Entities;

namespace PetGrubBakcend.Repositories.Address
{
    public interface IAddressRepository
    {
        Task<bool> IsexistingAddress(AddressCreateDto addressCreateDto, int addressId);
        Task CreateAddress(AddressUser address);
        Task<List<AddressUser>> GetUserAddresses();
        Task<AddressUser?> GetAddressbyId(int addressId,int userId);
        Task<bool> DeleteAddress(AddressUser address);
        

    }
}
