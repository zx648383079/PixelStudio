using SkiaSharp;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor.Layers
{
    public class ResizeLayer(IImageEditor editor) : IImageSource, ICommandLayer, IMouseState
    {


        private ImageBuffer? _surface;

        public bool IsVisible { get; set; } = false;
        public Rect Bound { get; private set; } = new();

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
            _surface?.Dispose();
            _surface = null;
        }

        public void Paint(IImageCanvas canvas)
        {
            if (_surface == null)
            {
                RenderSurface();
            }
            if (_surface == null)
            {
                return;
            }
            canvas.Draw(_surface);
        }

        private void RenderSurface()
        {
            var options = editor.Options;
            var bound = Bound;
            if (_surface is null)
            {
                if (bound.IsEmpty)
                {
                    return;
                }
                _surface = new ImageBuffer(editor.Size);
            } else
            {
                _surface.Resize(editor.Size);
            }
            _surface.Clear();
            _surface.DrawRect(bound, options.JointStrokePaint);
            var jointHalf = options.JointSize / 2;
            var jointX = bound.Left - jointHalf;
            var jointY = bound.Top - jointHalf;
            var widthHalf = bound.Width / 2;
            var heightHalf = bound.Height / 2;
            var paint = options.JointPaint;
            for (int i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (i == 1 && j == 1)
                    {
                        continue;
                    }
                    _surface.DrawRect(new Rect(jointX + i * widthHalf, 
                        jointY + j * heightHalf, options.JointSize, options.JointSize), paint);
                }
            }
        }

        

        public void PointerPressed(IMouseRoutedArgs args)
        {
        }

        public void PointerMoved(IMouseRoutedArgs args)
        {
        }

        public void PointerReleased(IMouseRoutedArgs args)
        {
        }

        public void Resize(Size size)
        {
        }

        public void With(IImageLayer layer)
        {
            Bound = layer.Source.Bound;
        }

        public void Dispose()
        {
            _surface?.Dispose();
        }
    }
}
