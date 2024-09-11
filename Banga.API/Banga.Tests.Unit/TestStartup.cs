using Banga.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Banga.Tests.Unit
{

    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public Mock<IPropertyRepository> PropertyRepository { get; private set; }
        public Mock<IPropertyPhotoRepository> PropertyPhotoRepository { get; private set; }
        public Mock<IPropertyOfferRepository> PropertyOfferRepository { get; private set; }

        public CustomWebApplicationFactory()
        {
            PropertyRepository = new Mock<IPropertyRepository>();
            PropertyPhotoRepository = new Mock<IPropertyPhotoRepository>();
            PropertyOfferRepository = new Mock<IPropertyOfferRepository>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove existing services if needed and add mocks
                services.AddTransient(_ => PropertyRepository.Object);
                services.AddTransient(_ => PropertyPhotoRepository.Object);
                services.AddTransient(_ => PropertyOfferRepository.Object);
            });
        }
    }
    //public class TestStartup<TStartup> where TStartup : class
    //{
    //    public MockRepository MockRepository { get; }
    //    private readonly TestServer _server;
    //    public HttpClient Client { get; }
    //   // public IConfiguration Configuration { get; }
    //    public Mock<IPropertyRepository> PropertyRepository {get; set;}
    //    public Mock<IPropertyPhotoRepository> PropertyPhotoRepository { get; set; }
    //    public Mock<IPropertyOfferRepository> PropertyOfferRepository { get; set; }

    //    public TestStartup()
    //    {
    //        MockRepository = new MockRepository(MockBehavior.Default);
    //        PropertyRepository = MockRepository.Create<IPropertyRepository>();
    //        PropertyPhotoRepository = MockRepository.Create<IPropertyPhotoRepository>();
    //        PropertyOfferRepository = MockRepository.Create<IPropertyOfferRepository>();

    //        _server = new TestServer
    //          (
    //              new WebHostBuilder()
    //              .UseStartup<TStartup>()
    //              .ConfigureAppConfiguration((context, config) =>
    //              {
    //                  config
    //                  .AddJsonFile("appsettings.json").AddJsonFile("appsettings.Development.json")
    //                  .AddEnvironmentVariables();
    //              })
    //              .ConfigureTestServices(services =>
    //              {
    //                  ConfigureServices(services);
    //              })
    //          );
    //       // Configuration = _server.Host.Services.GetService<IConfiguration>();

    //        Client = _server.CreateClient();
    //    }

    //    public void ConfigureServices(IServiceCollection services)
    //    {
    //        services.AddTransient(serviceProvider => PropertyRepository.Object);
    //        services.AddTransient(serviceProvider => PropertyPhotoRepository.Object);
    //        services.AddTransient(serviceProvider => PropertyOfferRepository.Object);
    //    }
    //}
}
