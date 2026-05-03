using SkiaSharp;
using System;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor.Layers
{
    public class SelectLayer : IImageSource, ICommandLayer, IMouseState
    {

        private readonly IImagePaint _paint = new ImagePaint(new()
        {
            Color = SKColors.Blue,
            StrokeWidth = 1,
            Style = SKPaintStyle.StrokeAndFill,
            ColorF = SKColors.Blue.WithAlpha(50)
        });
        private Point _start = new();
        private Point _last = new();

        public bool IsVisible { get; set; } = false;
        public Rect Bound => new(
                Math.Min(_start.X, _last.X), Math.Min(_start.Y, _last.Y),
                Math.Max(_start.X, _last.X), Math.Max(_start.Y, _last.Y)
                );

        public bool Contains(Point point)
        {
            return false;
        }

        public object? CreateThumbnail(Size size)
        {
            return null;
        }

        public void Invalidate()
        {
        }

        public void Paint(IImageCanvas canvas)
        {
            if (!IsVisible)
            {
                return;
            }
            canvas.DrawRect(Bound, 
                _paint);
        }

        public void PointerMoved(IMouseRoutedArgs args)
        {
            if (!IsVisible)
            {
                return;
            }
            _last = args.Position;
        }

        public void PointerPressed(IMouseRoutedArgs args)
        {
            IsVisible = true;
            _start = args.Position;
            _last = args.Position;
        }

        public void PointerReleased(IMouseRoutedArgs args)
        {
            IsVisible = false;
        }

        public void Resize(Size size)
        {
        }

        public void With(IImageLayer layer)
        {
        }

        public void Dispose()
        {
            _paint.Dispose();
        }
    }
}
