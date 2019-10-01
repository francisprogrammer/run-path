using Microsoft.Extensions.DependencyInjection;

namespace RP.App.Tasks.GetAlbumByUsers
{
    public static class GetAlbumsByUserDependencies
    {
        public static void AddGetAlbumsByUserDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IGetAlbumsByUser, GetAlbumsByUser>();
        }
    }
}