using System.Threading.Tasks;

namespace ZoDream.Shared.Interfaces
{
    public interface IPluginReader<T>
    {

        public Task<T?> ReadAsync(string fileName);

        public Task WriteAsync(string fileName, T data);
    }

    public interface IPluginPainter
    {
        public Task PaintAsync(IImageBuilder builder);
    }
}
