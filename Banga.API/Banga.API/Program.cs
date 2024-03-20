using Banga.Data;
using Banga.Logic.Extensions;
using Banga.Logic.Middleware;
using Banga.Logic.SignalR;
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
//app.UseMiddleware<ExceptionMiddleware>();   

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(corsPolicyBuilder => corsPolicyBuilder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins("http://localhost:4200")); 

app.UseAuthentication();
app.UseAuthorization();


app.UseHttpsRedirection();

app.MapControllers();
app.MapHub<PresenceHub>("hubs/presence");

app.Run();
