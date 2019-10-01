using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using RP.Api.Features.GetAlbumByUsers;
using RP.App.Common;
using RP.App.Tasks.GetAlbumByUsers;
using RP.App.Tasks.GetAlbums;
using RP.App.Tasks.GetPhotos;

namespace RP.Tests.Features
{
    public class GetAlbumByUserControllerTests
    {
        private IGetAlbums _stubGetAlbums;
        private IGetPhotos _stubGetPhotos;
        private GetAlbumByUserController _sut;

        [SetUp]
        public void SetUp()
        {
            _stubGetAlbums = Substitute.For<IGetAlbums>();
            _stubGetPhotos = Substitute.For<IGetPhotos>();

            var getAlbumsByUser = new GetAlbumsByUser(_stubGetAlbums, _stubGetPhotos);

            _sut = new GetAlbumByUserController(getAlbumsByUser);
        }

        [TestCase(1, 2, "dummy album title", 5, "dummy photo title", "dummy photo url", "dummy photo thumbnail url")]
        [TestCase(3, 4, "other dummy album title", 6, "other dummy photo title", "other dummy photo url", "other dummy photo thumbnail url")]
        public async Task Returns_the_albums_and_photos_relevant_to_a_single_user(int userId, int albumId, string albumTitle, int photoId, string photoTitle, string photoUrl, string photoThumbnailUrl)
        {
            // arrange
            _stubGetAlbums
                .Get()
                .Returns(
                    Response<IEnumerable<AlbumDto>>
                        .Success(new[]
                        {
                            new AlbumDto(1, 2, "dummy album title"),
                            new AlbumDto(3, 4, "other dummy album title")
                        }));

            _stubGetPhotos
                .Get()
                .Returns(
                    Response<IEnumerable<PhotoDto>>
                        .Success(new[]
                        {
                            new PhotoDto(2, 5, "dummy photo title", "dummy photo url", "dummy photo thumbnail url"),
                            new PhotoDto(4, 6, "other dummy photo title", "other dummy photo url", "other dummy photo thumbnail url")
                        }));

            // act
            var result = await _sut.Get(userId);

            // assert
            Assert.That(result is OkObjectResult, "expected ok response");

            var response = (IEnumerable<Album>) (result as OkObjectResult).Value;

            Assert.That(response.ElementAt(0).Id, Is.EqualTo(albumId));
            Assert.That(response.ElementAt(0).Title, Is.EqualTo(albumTitle));

            Assert.That(response.ElementAt(0).Photos.ElementAt(0).Id, Is.EqualTo(photoId));
            Assert.That(response.ElementAt(0).Photos.ElementAt(0).Title, Is.EqualTo(photoTitle));
            Assert.That(response.ElementAt(0).Photos.ElementAt(0).Url, Is.EqualTo(photoUrl));
            Assert.That(response.ElementAt(0).Photos.ElementAt(0).ThumbnailUrl, Is.EqualTo(photoThumbnailUrl));
        }

        [TestCase(0)]
        [TestCase(null)]
        public async Task Returns_bad_request_when_user_id_is_not_acceptable(int? userId)
        {
            // act
            var result = await _sut.Get(userId);

            // assert
            Assert.That(result is BadRequestObjectResult, "expected bad request response");

            var response = (string) (result as BadRequestObjectResult).Value;

            Assert.That(response, Is.EqualTo("User id is not valid"));
        }

        [Test]
        public async Task Returns_error_message_when_fails_to_retrieve_albums()
        {
            // arrange
            var userId = 1;

            var anyReason = "any album reason";
            
            _stubGetAlbums
                .Get()
                .Returns(Response<IEnumerable<AlbumDto>>.Failed(anyReason));
            
            // act
            var result = await _sut.Get(userId);
            
            // assert
            Assert.That(result is BadRequestObjectResult, "expected bad request response");

            var response = (string) (result as BadRequestObjectResult).Value;

            Assert.That(response, Is.EqualTo(anyReason));
        }
        
        [Test]
        public async Task Returns_error_message_when_fails_to_retrieve_photos()
        {
            // arrange
            var userId = 1;

            var anyReason = "any photo reason";
            
            _stubGetAlbums
                .Get()
                .Returns(
                    Response<IEnumerable<AlbumDto>>
                        .Success(Enumerable.Empty<AlbumDto>()));
            
            _stubGetPhotos
                .Get()
                .Returns(Response<IEnumerable<PhotoDto>>.Failed(anyReason));
            
            // act
            var result = await _sut.Get(userId);
            
            // assert
            Assert.That(result is BadRequestObjectResult, "expected bad request response");

            var response = (string) (result as BadRequestObjectResult).Value;

            Assert.That(response, Is.EqualTo(anyReason));
        }
    }
}