using System.Threading.Tasks;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;

namespace ZoDream.PixelStudio.Plugins
{
    public class ImageFactoryReader : IPluginReader<IImageData>
    {
        public Task<IImageData?> ReadAsync(string fileName)
        {
            return Task.FromResult(new FileImageData(fileName) as IImageData);
        }


        public Task WriteAsync(string fileName, IImageData data)
        {
            data.ToBitmap()?.SaveAs(fileName);
            return Task.CompletedTask;
        }

    }
}
