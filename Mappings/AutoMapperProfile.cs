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

            CreateMap<CartReadResponseDto,CartItem>().ReverseMap();
            CreateMap<AddToCartDto,CartItem>().ReverseMap();
            CreateMap<CartItem, CartReadDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Product.Title))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Product.Brand))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Product.Description))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product.ImageUrl))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));
            CreateMap<CartReadDto, CartItem>();

            CreateMap<Product, CartItem>();
            CreateMap<Product, CartReadDto>()
                 .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                    .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            CreateMap<AddressCreateDto, AddressUser>();
            CreateMap<AddressUser, AddressReadDto>();



        }
    }
}
