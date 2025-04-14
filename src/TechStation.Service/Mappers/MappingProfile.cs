using AutoMapper;
using TechStation.Domain.Entities;
using TechStation.Service.DTOs.Banners;
using TechStation.Service.DTOs.Brends;
using TechStation.Service.DTOs.CartItems;
using TechStation.Service.DTOs.Catalogs;
using TechStation.Service.DTOs.Categories;
using TechStation.Service.DTOs.Favourites;
using TechStation.Service.DTOs.OrderDetails;
using TechStation.Service.DTOs.Orders;
using TechStation.Service.DTOs.Payments;
using TechStation.Service.DTOs.Products;
using TechStation.Service.DTOs.UserRoles;
using TechStation.Service.DTOs.Users;

namespace TechStation.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Folder Name :  User
        CreateMap<User, UserForUpdateDto>().ReverseMap();
        CreateMap<User, UserForResultDto>().ReverseMap();
        CreateMap<User, UserForCreationDto>().ReverseMap();

        // Folder Name : UserRole
        CreateMap<UserRole, UserRoleForCreationDto>().ReverseMap();
        CreateMap<UserRole, UserRoleForResultDto>().ReverseMap();
        CreateMap<UserRole, UserRoleForUpdateDto>().ReverseMap();

        //Folder Name :  Catalog
        CreateMap<Catalog,CatalogForCreationDto>().ReverseMap();
        CreateMap<Catalog, CatalogForResultDto>().ReverseMap();
        CreateMap<Catalog, CatalogForUpdateDto>().ReverseMap();
        CreateMap<Catalog, CategoryForResultDto>().ReverseMap();

        //Folder Name :  Category
        CreateMap<Category,CategoryForCreationDto>().ReverseMap();
        CreateMap<Category,CategoryForResultDto>().ReverseMap();
        CreateMap<Category,CategoryForUpdateDto>().ReverseMap();

        //Folder Name :  Product
        CreateMap<Product,ProductForCreationDto>().ReverseMap();
        CreateMap<Product,ProductForResultDto>().ReverseMap();
        CreateMap<ProductForResultDto, Product>().ReverseMap();
        CreateMap<Product,ProductForUpdateDto>().ReverseMap();
        CreateMap<Product, OrderForResultDto>().ReverseMap();

        //Folder Name :  Order
        CreateMap<Order,OrderForCreationDto>().ReverseMap();
        CreateMap<Order, OrderForResultDto>().ReverseMap();
        CreateMap<Order, OrderForUpdateDto>().ReverseMap();


        //Folder Name :  OrderDetail
        CreateMap<OrderDetail, OrderDetailForCreationDto>().ReverseMap();
        CreateMap<OrderDetail, OrderDetailForResultDto>().ReverseMap();
        CreateMap<OrderDetail, OrderDetailForUpdateDto>().ReverseMap();

        //Folder Name :  Payment
        CreateMap<Payment, PaymentForCreationDto>().ReverseMap();
        CreateMap<Payment,PaymentForResultDto>().ReverseMap();
        CreateMap<Payment,PaymentForUpdateDto>().ReverseMap();

        //Folder Name :  Brand
        CreateMap<Brend,BrendForCreationDto>().ReverseMap();
        CreateMap<Brend,BrendForResultDto>().ReverseMap();
        CreateMap<Brend,BrendForUpdateDto>().ReverseMap();
        CreateMap<BrendForCreationDto, BrendForResultDto>().ReverseMap();

        //Folder Name : Banner
        CreateMap<Banner,BannerForCreationDto>().ReverseMap();
        CreateMap<Banner,BannerForResultDto>().ReverseMap();
        CreateMap<Banner,BannerForUpdateDto>().ReverseMap();    

        //Folder Name : CartItem
        CreateMap<CartItem,CartItemForCreationDto>().ReverseMap();
        CreateMap<CartItem,CartItemForResultDto>().ReverseMap();
        CreateMap<CartItem,CartItemForUpdateDto>().ReverseMap();

        //Folder Name : Favourite
        CreateMap<Favourite,FavouriteForCreationDto>().ReverseMap();
        CreateMap<Favourite,FavouriteForResultDto>().ReverseMap();
    }
}
