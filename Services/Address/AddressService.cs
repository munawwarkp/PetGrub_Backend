using AutoMapper;
using PetGrubBakcend.ApiResponse;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Entities;
using PetGrubBakcend.Repositories.Address;

namespace PetGrubBakcend.Services.Address
{
    public class AddressService:IAddressService
    {
        private readonly IAddressRepository repository;
        private readonly IMapper _mapper;
        public AddressService(IAddressRepository addressRepository,IMapper mapper)
        {
            repository = addressRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<AddressReadDto>> CreateAddress(AddressCreateDto addressCreateDto,int userId)
        {
            try
            {
                bool isExist = await repository.IsexistingAddress(addressCreateDto, userId);

                var newAddress = _mapper.Map<AddressUser>(addressCreateDto);
                newAddress.UserId = userId;

                if (!isExist)
                {
                     await repository.CreateAddress(newAddress);
                    var mappedAddress = _mapper.Map<AddressReadDto>(newAddress);
                    return new ApiResponse<AddressReadDto>
                    {
                        StatusCode = 200,
                        Message = "Address added succesfully",
                        Data = mappedAddress
                    };
                }

                return new ApiResponse<AddressReadDto>
                {
                    StatusCode = 409,
                    Message = "Address already exists",
                };

            }
            catch(Exception ex)
            {
                return new ApiResponse<AddressReadDto>
                {
                    StatusCode = 500,
                    Message = $"Error occured while creating Address : {ex.Message}"
                };
            }

        }

        public async Task<ApiResponse<List<AddressReadDto>>> GetAddresses()
        {
            try
            {
                var addresses = await repository.GetUserAddresses();
                var mappedAddresses = _mapper.Map<List<AddressReadDto>>(addresses);

                return new ApiResponse<List<AddressReadDto>>
                {
                    StatusCode = 200,
                    Message = "succesfully fetched addresses",
                    Data = mappedAddresses
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<AddressReadDto>>
                {
                    StatusCode = 500,
                    Message = $"Error occured while fetching addresses :{ex.Message} "
                };
            }
        }
        public async Task<ApiResponse<object>> DeleteAddress(int addressId,int userId)
        {
            try
            {
               var address = await repository.GetAddressbyId(addressId,userId);
                if(address == null)
                {
                    return new ApiResponse<object>
                    {
                        StatusCode = 404,
                        Message = "No address found"
                    };
                }
                var mappedAddress = _mapper.Map<AddressUser>(address);
                var data = _mapper.Map<AddressReadDto>(address);
               await repository.DeleteAddress(mappedAddress);
                return new ApiResponse<object>
                {
                    StatusCode = 200,
                    Message = "Deleted succesfully",
                    Data = data     
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<object>
                {
                    StatusCode = 500,
                    Message = $"Error occured while deleting address : {ex.Message}"
                };
            }
        }
    }
}
