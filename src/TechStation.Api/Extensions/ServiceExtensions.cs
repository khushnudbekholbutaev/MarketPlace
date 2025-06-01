using TechStation.Data.IRepositories;
using TechStation.Data.Repositories;
using TechStation.Service.Interfaces.Auths;
using TechStation.Service.Interfaces.Banners;
using TechStation.Service.Interfaces.Brends;
using TechStation.Service.Interfaces.CartItems;
using TechStation.Service.Interfaces.Catalogs;
using TechStation.Service.Interfaces.Categories;
using TechStation.Service.Interfaces.Favourites;
using TechStation.Service.Interfaces.Files;
using TechStation.Service.Interfaces.OrderDetails;
using TechStation.Service.Interfaces.Orders;
using TechStation.Service.Interfaces.Payments;
using TechStation.Service.Interfaces.Products;
using TechStation.Service.Interfaces.Users;
using TechStation.Service.Services.Auths;
using TechStation.Service.Services.Banners;
using TechStation.Service.Services.Brends;
using TechStation.Service.Services.CartItems;
using TechStation.Service.Services.Catalogs;
using TechStation.Service.Services.Categories;
using TechStation.Service.Services.Favourites;
using TechStation.Service.Services.Files;
using TechStation.Service.Services.OrderDetails;
using TechStation.Service.Services.Orders;
using TechStation.Service.Services.Payments;
using TechStation.Service.Services.Products;
using TechStation.Service.Services.Users;

namespace TechStation.Api.Extensions;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        //Folder Name: Generic Reporitory
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        //Folder Name: User Service
        services.AddScoped<IUserService, UserService>();

        // Folder Name : Auth Service
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        // Folder Name : User Role Service
        services.AddScoped<IUserRoleService, UserRoleService>();

        //Folder Name: Catalog Service
        services.AddScoped<ICatalogService, CatalogService>();

        //Folder Name: Category
        services.AddScoped<ICategoryService,CategoryService>();

        //Folder Name: Product
        services.AddScoped<IProductService, ProductService>();

        //Folder Name: Order
        services.AddScoped<IOrderService, OrderService>();

        //Folder Name: OrderDetail
        services.AddScoped<IOrderDetailService,OrderDetailService>();

        //Folder Name: Payment
        services.AddScoped<IPaymentService, PaymentService>();

        //Folder Name: FileUploadService
        services.AddScoped<IFileUploadService, FileUploadService>();

        //Folder Name : Brend
        services.AddScoped<IBrendService,BrendService>();

        //Forder Name : Banner
        services.AddScoped<IBannerService,BannerService>();

        //Folder Name : CartItem
        services.AddScoped<ICartItemService,CartItemService>();

        //Folder Name : Favourite
        services.AddScoped<IFavouriteService,FavouriteService>();
    }
}
