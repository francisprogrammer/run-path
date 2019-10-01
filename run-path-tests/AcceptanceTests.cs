using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using RP.App.Tasks.GetAlbumByUsers;

namespace RP.Tests
{
    public class AcceptanceTests
    {
        private ApiWebApplicationFactory _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void GivenARequestToTheController()
        {
            _factory = new ApiWebApplicationFactory();
            _client = _factory.CreateClient();
        }
        
        [Test]
        public async Task Returns_the_albums_and_photos_relevant_to_a_single_user()
        {
            // arrange
            var userId = 1;
            
            // act
            var result = await _client.GetAsync($"/users/{userId}/albums");
            
            // assert
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var albums = JsonConvert.DeserializeObject<IEnumerable<Album>>(await result.Content.ReadAsStringAsync());
            
            Assert.That(albums.Count(), Is.EqualTo(10));
            Assert.That(albums.SelectMany(album => album.Photos).Count(), Is.EqualTo(500));
        }
    }
    
    class ApiWebApplicationFactory : WebApplicationFactory<Api.Startup> {}
}