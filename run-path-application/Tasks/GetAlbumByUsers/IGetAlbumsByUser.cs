using System.Collections.Generic;
using System.Threading.Tasks;
using RP.App.Common;

namespace RP.App.Tasks.GetAlbumByUsers
{
    public interface IGetAlbumsByUser
    {
        Task<Response<IEnumerable<Album>>> Get(GetAlbumsByUserRequest request);
    }
}