using AutoBogus;
using Banga.Data.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using System.Net;

namespace Banga.Tests.Unit.Controllers
{
    [Collection("Fixture collection")]
    public class PropertyControllerTests
    {
        //private readonly TestStartup<Program> _fixture;
        private readonly CustomWebApplicationFactory<Program> _fixture;
        private readonly HttpClient _client;

        public PropertyControllerTests(CustomWebApplicationFactory<Program> fixture)
        {
            _fixture = fixture;
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task ShouldGetPropertyById()
        {
            //Arrange
            var propertyId = AutoFaker.Generate<int>(); ; 
            var property = AutoFaker.Generate<Property>();
            var photos = AutoFaker.Generate<PropertyPhoto>(2);
            var offers = AutoFaker.Generate<PropertyOffer>(2);

            _fixture.PropertyRepository
                .Setup(x => x.GetPropertyById(It.Is<int>(x => x == propertyId))).ReturnsAsync(property);

            _fixture.PropertyPhotoRepository
                .Setup(x => x.GetPropertyPhotosByPropertyId(It.Is<int>(x => x == propertyId))).ReturnsAsync(photos);


            _fixture.PropertyOfferRepository
                .Setup(x => x.GetOffersByPropertyId(It.Is<int>(x => x == propertyId))).ReturnsAsync(offers);

  
            //Act
            var response = await _client.GetAsync($"/api/Property/{propertyId}");


            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
