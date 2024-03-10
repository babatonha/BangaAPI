﻿
using Banga.Data;
using Banga.Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Banga.Logic.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true;
            })
                .AddRoles<AppRole>()
                .AddRoleManager<RoleManager<AppRole>>()
                .AddEntityFrameworkStores<DatabaseContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]!)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true, 
                    };

                    //options.Events = new JwtBearerEvents
                    //{
                    //    OnMessageReceived = context =>
                    //    {
                    //        var accessToken = context.Request.Query["access_token"];

                    //        var path = context.HttpContext.Request.Path;
                    //        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                    //        {
                    //            context.Token = accessToken;
                    //        }

                    //        return Task.CompletedTask;
                    //    }
                    //};
                });

            //services.AddAuthorization(opt =>
            //{
            //    opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            //    opt.AddPolicy("ModeratePhotoRole", policy => policy.RequireRole("Admin", "Moderator"));
            //});

            return services;
        }
    }
}
