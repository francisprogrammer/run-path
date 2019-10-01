using System.Collections.Generic;
using System.Threading.Tasks;
using RP.App.Common;

namespace RP.App.Tasks.GetPhotos
{
    class GetPhotos : IGetPhotos
    {
        private const string URL = "http://jsonplaceholder.typicode.com/photos"; // This would usually be in the app settings so I can change depending on environment
        private const string ErrorMessage = "Error retrieving photos, oh no!\nPlease try again later";

        public async Task<Response<IEnumerable<PhotoDto>>> Get()
        {
            // Ideally I would have caching here to prevent unnecessary calls to api and help improve performance
            return await HttpClientHelper.Get<IEnumerable<PhotoDto>>(URL, ErrorMessage);
        }
    }
}