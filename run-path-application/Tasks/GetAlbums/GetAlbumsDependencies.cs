using Microsoft.Extensions.DependencyInjection;

namespace RP.App.Tasks.GetAlbums
{
    public static class GetAlbumsDependencies
    {
        public static void AddGetAlbumsDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IGetAlbums, GetAlbums>();
        }
    }
}