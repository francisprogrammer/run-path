using Microsoft.Extensions.DependencyInjection;

namespace RP.App.Tasks.GetPhotos
{
    public static class GetPhotosDependencies
    {
        public static void AddGetPhotosDependencies(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IGetPhotos, GetPhotos>();
        }
    }
}