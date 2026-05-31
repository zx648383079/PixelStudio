using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.PixelArt
{
    public class LineCommand(IImageProperty option) : IImageSource
    {
        private readonly IThumbnailBuilder _thumbnail = option.Options.CreateThumbnail();

        public Rect Bound => Rect.Create(From, To);

        public Point From { get; set; }
        public Point To { get; set; }

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
            canvas.DrawLine(From, To, option.Options.ForegroundPaint);
        }

        public void Dispose()
        {
        }
    }
}
