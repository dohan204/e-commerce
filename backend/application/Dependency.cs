using application.cases.Commands.Authentication;
using application.cases.Commands.Users;
using application.cases.Queries.Authentication;
using application.cases.Queries.Users;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace application
{
    public static class AppDependency
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
        {
            services.AddFluentValidationAutoValidation();
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