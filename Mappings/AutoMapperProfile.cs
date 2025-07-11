using AutoMapper;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Entities;

namespace PetGrubBakcend.Mappings
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserRegistrationDto, User>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<AddCategoryDto, Category>().ForMember(dest => dest.Id, opt=>opt.Ignore());
            CreateMap<ProductDto, Product>().ForMember(dest => dest.Id,opt => opt.Ignore());
            CreateMap<PaginatedResultDto<ProductDto>, Product>();
            CreateMap<PaginationParamsDto, Product>();
        }
    }
}
