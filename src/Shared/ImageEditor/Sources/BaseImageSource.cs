using SkiaSharp;
using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor.Sources
{
    public abstract class BaseImageSource(IImageEditor editor) : 
        IImageStyleSource, IReadOnlyStyle
    {
        protected readonly BitmapBuilder Thumbnail = new();
        protected IImageEditor Editor { get; private set; } = editor;
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        /// <summary>
        /// 旋转角度0 - 360
        /// </summary>
        public float Rotate { get; set; }

        public float ScaleX { get; set; } = 1;
        public float ScaleY { get; set; } = 1;

        public float ShearX { get; set; }
        public float ShearY { get; set; }

        public virtual SKRect Bound => SKRect.Create(X, Y, Width, Height);

        

        public virtual SKBitmap? CreateThumbnail(SKSize size)
        {
            return null;
        }
        public virtual void Dispose()
        {
            Thumbnail.Dispose();
        }

        public virtual bool Contains(float x, float y)
        {
            var offsetX = x - X;
            if (offsetX < 0 || offsetX > Width)
            {
                return false;
            }
            var offsetY = y - Y;
            if (offsetY < 0 || offsetY > Height)
            {
                return false;
            }
            return true;
        }

        public bool Contains(SKPoint point)
        {
            return Bound.Contains(point);
        }

        public virtual void Paint(IImageCanvas canvas)
        {
        }

        public abstract void Paint(IImageCanvas canvas, IImageStyle computedStyle);
    }
}
