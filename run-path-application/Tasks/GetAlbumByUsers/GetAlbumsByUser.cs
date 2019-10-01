using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RP.App.Common;
using RP.App.Tasks.GetAlbums;
using RP.App.Tasks.GetPhotos;

namespace RP.App.Tasks.GetAlbumByUsers
{
    class GetAlbumsByUser : IGetAlbumsByUser
    {
        private readonly IGetAlbums _getAlbums;
        private readonly IGetPhotos _getPhotos;

        public GetAlbumsByUser(IGetAlbums getAlbums, IGetPhotos getPhotos)
        {
            _getAlbums = getAlbums;
            _getPhotos = getPhotos;
        }

        public async Task<Response<IEnumerable<Album>>> Get(GetAlbumsByUserRequest request)
        {
            var albumsTask = _getAlbums.Get();
            var photosTask = _getPhotos.Get();

            await Task.WhenAll(albumsTask, photosTask);

            var albums = albumsTask.Result;

            if (!albums.IsSuccess)
            {
                return Response<IEnumerable<Album>>.Failed(albums.FailureMessage);
            }

            var photos = photosTask.Result;

            if (!photos.IsSuccess)
            {
                return Response<IEnumerable<Album>>.Failed(photos.FailureMessage);
            }
            
            var userAlbums = GetUserAlbums(request.UserId, albums.Value, photos.Value);

            return Response<IEnumerable<Album>>.Success(userAlbums);
        }

        private static IEnumerable<Album> GetUserAlbums(int userId, IEnumerable<AlbumDto> albums, IEnumerable<PhotoDto> photos)
        {
            return albums
                .Where(albumDto => albumDto.UserId.Equals(userId))
                .Select(albumDto => 
                    new Album(
                        albumDto.Id,
                        albumDto.UserId,
                        albumDto.Title,
                        photos
                            .Where(photoDto => photoDto.AlbumId.Equals(albumDto.Id))
                            .Select(photoDto => new Photo(albumDto.Id, photoDto.Id, photoDto.Title, photoDto.Url, photoDto.ThumbnailUrl))));
        }
    }
}