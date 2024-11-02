using Banga.Data;
using Banga.Logic.Extensions;
using Banga.Logic.Middleware;
using Banga.Logic.SignalR;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DatabaseContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors();

var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseStaticFiles();
app.UseRouting();

app.UseCors(corsPolicyBuilder => corsPolicyBuilder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins("https://easyestate.online", "http://easyestate.online", "http://localhost:4200", "http://localhost:1920"));


app.UseAuthentication();
app.UseAuthorization();


app.UseHttpsRedirection();

app.MapControllers();

app.MapHub<ChatHub>("/chat-hub");

//app.MapHub<PresenceHub>("hubs/presence");
//app.MapHub<MessageHub>("hubs/message");

//using var scope = app.Services.CreateScope();
//var services = scope.ServiceProvider;
//try
//{
//    var context = services.GetRequiredService<DatabaseContext>();
//    await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE [Connections]");
//}
//catch (Exception)
//{

//    throw;
//}

app.Run();

