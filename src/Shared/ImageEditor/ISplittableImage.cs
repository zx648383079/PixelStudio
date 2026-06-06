using SkiaSharp;
using System.Threading;
using System.Threading.Tasks;
using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor
{
    public interface ISplittableImage
    {

        public float Width { get; }
        public float Height { get; }

        public void CopyTo(SKCanvas canvas, SKRect source, SKRect dest, SKPaint? paint = null);
        public Task<SKPath[]> GetContourAsync(CancellationToken token = default);

        public IImageSource? Split(SKPath path);
    }
}
