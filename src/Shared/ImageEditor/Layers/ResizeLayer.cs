using System.Linq;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor.Layers
{
    public class ResizeLayer(IImageEditor editor) : IImageSource, ICommandLayer, IMouseState
    {


        private ImageBuffer? _surface;
        private IImageLayer[] _items = [];
        private Point _lastPoint = new();

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
            var borderPaint = options.JointStrokePaint;
            _surface.DrawRect(bound, borderPaint);
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
                    var rect = new Rect(jointX + i * widthHalf,
                        jointY + j * heightHalf, options.JointSize, options.JointSize);
                    _surface.DrawRect(rect, paint);
                    _surface.DrawRect(rect, borderPaint);
                }
            }
        }

        

        public void PointerPressed(IMouseRoutedArgs args)
        {
            if (!Bound.Contains(args.Position))
            {
                return;
            }
            _lastPoint = args.Position;
        }

        public void PointerMoved(IMouseRoutedArgs args)
        {
            if (_items.Length == 0 || args.State.HasFlag(PointerState.Released))
            {
                return;
            }
            var current = args.Position;
            var offset = current - _lastPoint;
            foreach (var item in _items)
            {
                item.Move(offset);
            }
            Bound += offset;
            _lastPoint = current;
            RenderSurface();
            editor.Initialize();
        }

        public void PointerReleased(IMouseRoutedArgs args)
        {
        }

        public void Resize(Size size)
        {
        }

        public void Initialize(IImageLayer[] items)
        {
            editor.Layer.SelectedItems = items;
            _items = items;
            Bound = Rect.Create(items.Select(i => i.Source.Bound));
        }

        public void Dispose()
        {
            _surface?.Dispose();
        }
    }
}
