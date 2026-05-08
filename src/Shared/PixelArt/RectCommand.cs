using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.PixelArt
{
    public class RectCommand(IImageProperty option) : IImageSource
    {
        private readonly IThumbnailBuilder _thumbnail = option.Options.CreateThumbnail();
        public Rect Bound => throw new System.NotImplementedException();

        public bool Contains(Point point)
        {
            return Bound.Contains(point);
        }

        public object? CreateThumbnail(Size size)
        {
            return _thumbnail.Mutate(size, canvas => {

            });
        }



        public void Paint(IImageCanvas canvas)
        {
        }

        public void Dispose()
        {
        }
    }
}
