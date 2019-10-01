using System.Collections.Generic;
using RP.App.Common;

namespace RP.App.Tasks.GetPhotos
{
    public interface IGetPhotos : IResource<Response<IEnumerable<PhotoDto>>> {}
}