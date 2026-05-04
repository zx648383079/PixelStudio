using System.Threading.Tasks;

namespace ZoDream.Shared.Interfaces
{
    public interface IImageService
    {
        public T Get<T>();
        public Task<IImageSource?> AskTextAsync();
    }
}
