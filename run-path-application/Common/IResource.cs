using System.Threading.Tasks;

namespace RP.App.Common
{
    public interface IResource<T> where T : class
    {
        Task<T> Get();
    }
}