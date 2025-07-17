using AutoMapper;
using PetGrubBakcend.DTOs;
using PetGrubBakcend.Entities;
using PetGrubBakcend.Result;

namespace PetGrubBakcend.Mappings
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserRegistrationDto, User>().ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
            CreateMap<AddCategoryDto, Category>().ForMember(dest => dest.Id, opt=>opt.Ignore());
            CreateMap<ProductDto, Product>().ForMember(dest => dest.Id,opt => opt.Ignore()).ReverseMap();
            CreateMap<Product, ProductReadingDto>().ReverseMap();
            CreateMap<Result<ProductDto>, ProductDto>().ReverseMap();
            CreateMap<WishListDto, Wishlist>().ReverseMap();
            CreateMap<Wishlist, Product>().ReverseMap();

        }
    }
}
