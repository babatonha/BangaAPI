using Banga.Data.Repositories;
using Banga.Domain.Interfaces.Repositories;
using Banga.Domain.Interfaces.Services;
using Banga.Domain.Models;
using Banga.Logic.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Banga.Logic.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<IPropertyPhotoService, PropertyPhotoService>();
            services.AddScoped<IPropertyLocationService, PropertyLocationService>();
            services.AddScoped<IPropertyTypeService, PropertyTypeService>();
            services.AddScoped<ILawFirmService, LawFirmService>();
            services.AddScoped<ICloudinaryPhotoService, CloudinaryPhotoService>();



            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IPropertyPhotoRepository, PropertyPhotoRepository>();
            services.AddScoped<IPropertyLocationRepository, PropertyLocationRepository>();
            services.AddScoped<IPropertyTypeRepository, PropertyTypeRepository>();
            services.AddScoped<ILawFirmRepository, LawFirmRepository>();

            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
    

            return services;
        }
    }
}
