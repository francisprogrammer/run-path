using System.Collections.Generic;
using System.Threading.Tasks;
using RP.App.Common;

namespace RP.App.Tasks.GetAlbums
{
    class GetAlbums : IGetAlbums
    {
        private const string URL = "http://jsonplaceholder.typicode.com/albums"; // This would usually be in the app settings so I can change depending on environment
        private const string ErrorMessage = "Error retrieving albums, oh no!\nPlease try again later";
        
        public async Task<Response<IEnumerable<AlbumDto>>> Get()
        {
            // Ideally I would have caching here to prevent unnecessary calls to api and help improve performance
            return await HttpClientHelper.Get<IEnumerable<AlbumDto>>(URL, ErrorMessage);
        }
    }
}