using Banga.Data.Repositories;
using Banga.Domain.Interfaces.Repositories;
using Banga.Domain.Interfaces.Services;
using Banga.Domain.Mappers;
using Banga.Domain.Models;
using Banga.Logic.Services;
using Banga.Logic.SignalR;
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
            services.AddScoped<IPropertyOfferService, PropertyOfferService>();
            services.AddScoped<IPropertyLocationService, PropertyLocationService>();
            services.AddScoped<IPropertyTypeService, PropertyTypeService>();
            services.AddScoped<ILawFirmService, LawFirmService>();
            services.AddScoped<ICloudinaryPhotoService, CloudinaryPhotoService>();
            services.AddScoped<IBuyerListingService, BuyerListingService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<ILikesService, LikesService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IMailjetService, MailjetService>();



            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped<IPropertyOfferRepository, PropertyOfferRepository>();
            services.AddScoped<IPropertyPhotoRepository, PropertyPhotoRepository>();
            services.AddScoped<IPropertyLocationRepository, PropertyLocationRepository>();
            services.AddScoped<IPropertyTypeRepository, PropertyTypeRepository>();
            services.AddScoped<ILawFirmRepository, LawFirmRepository>();
            services.AddScoped<IBuyerListingRepository, BuyerListingRepository>(); 
            services.AddScoped<ILikesRepository, LikesRepository>();

            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.Configure<MailjetSettings>(config.GetSection("MailjetSettings"));
            services.Configure<EmailSettings>(config.GetSection("SMTP"));
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddSignalR();

            services.AddSingleton<PresenceTracker>();

            return services;
        }
    }
}
