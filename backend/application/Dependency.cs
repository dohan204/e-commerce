using application.cases.Commands.Authentication;
using application.cases.Commands.Users;
using application.cases.Queries.Authentication;
using application.cases.Queries.Users;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using application.cases.Commands.Product;
using application.cases.Commands.Categories;
using application.cases.Commands.Orders;
using Serilog;
using application.cases.Queries.Carts;
using application.cases.Commands.Reviews;
using application.cases.Commands.Addresses;
namespace application
{
    public static class AppDependency
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Hour)
                .CreateLogger();
           // services.AddFluentValidationAutoValidation();
           // đăng ký nhiều cái validation trong project
            services.AddValidatorsFromAssemblyContaining<ApplicationMarker>();
            services.AddScoped<CreateAddressHandler>();
            services.AddScoped<CreateReviewHandler>();
            services.AddScoped<UpdateProductImageHandler>();
            services.AddScoped<GetCartByUserHandler>();

            services.AddScoped<CreateCategoryHandler>();
            services.AddScoped<CreateCategoryHandler>();
            services.AddScoped<CreateOrderHandler>();
            services.AddScoped<CreateCategoryHandler>();
            services.AddScoped<RemoveUserHandler>();
            services.AddScoped<GetUserLoginHandler>();
            services.AddScoped<EmailConfirmHanler>();
            // services.AddScoped<CreateUserHandler>();
            services.AddScoped<HandlerGetUserById>();
            services.AddScoped<GetAllUserQuery>();
            services.AddScoped<CreateUserHandler>();
            // services.AddMediatR()
            return services;
        }
    }
}