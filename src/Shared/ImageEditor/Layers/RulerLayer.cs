using SkiaSharp;
using System.Collections.Generic;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor.Layers
{
    /// <summary>
    /// 标尺
    /// </summary>
    /// <param name="editor"></param>
    public class RulerLayer : IImageSource, ICommandLayer
    {
        public RulerLayer(IImageEditor editor)
        {
            _editor = editor;
            Invalidate();
        }
        private readonly IImageEditor _editor;
        private readonly int _ruleSize = 30;
        private readonly int _gap = 10;
        private ImageBuffer? _surface;
        private IImagePaint _background = new ImagePaint(new()
        {
            ColorF = SKColors.LightGray,
            Style = SKPaintStyle.Fill,
        });
        private IImagePaint _foreground = ImagePaint.CreateBorder(Color.Black);
        private IImagePaint _font = new FontImagePaint(new(SKTypeface.Default, 8), SKTextAlign.Center, new()
        {
            IsStroke = false,
            ColorF = SKColors.Black,
            IsAntialias = true,
        });

        public IList<int> HorizontalLines = [];
        public IList<int> VerticalLines = [];
        public bool IsVisible { get; set; } = true;
        public Rect Bound => new();

        public bool Contains(Point point)
        {
            return false;
        }

        public object? CreateThumbnail(Size size)
        {
            return null;
        }

        public void Resize(Size size)
        {
            Invalidate();
        }

        public void Invalidate()
        {
            _surface?.Dispose();
            _surface = null;
        }

        public void With(IImageLayer layer)
        {
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
            var size = _editor.Size;
            if (size.Width == 0 || size.Height == 0)
            {
                return;
            }
            _surface = new(new Size(size.Width + _ruleSize, size.Height + _ruleSize));
            _surface.Clear();
            _surface.DrawRect(new Rect(_ruleSize, 0, size.Width, _ruleSize), _background);
            _surface.DrawRect(new Rect(0, _ruleSize, _ruleSize, size.Height), _background);
            var i = 0;
            while (true)
            {
                var x = i * _gap;
                if (x > size.Width)
                {
                    break;
                }
                var toX = x + _ruleSize;
                _surface.DrawLine(new(toX, 0), new(toX, _ruleSize * ParseScale(i)), _foreground);
                if (i % 10 == 0)
                {
                    _surface.Draw(x.ToString(), new(toX, _ruleSize * .8f), _font);
                }
            }
            i = 0;
            while (true)
            {
                var y = i * _gap;
                if (y > size.Height)
                {
                    break;
                }
                var toY = y + _ruleSize;
                _surface.DrawLine(new(0, toY), new(_ruleSize * ParseScale(i), toY), _foreground);
                if (i % 10 == 0)
                {
                    _surface.Draw(y.ToString(), new(_ruleSize * .8f, toY), _font);
                }
            }
            foreach (var item in HorizontalLines)
            {
                _surface.DrawLine(new(0, item), new(_ruleSize + size.Width, item), _foreground);
                _surface.Draw(item.ToString(), new(_ruleSize + size.Width / 2, item), _font);
            }

            foreach (var item in VerticalLines)
            {
                _surface.DrawLine(new(item, 0), new(item, _ruleSize + size.Height), _foreground);
                _surface.Draw(item.ToString(), new(item, _ruleSize + size.Height / 2), _font);
            }
        }

        private static float ParseScale(int value)
        {
            if (value % 10 == 0)
            {
                return .6f;
            }
            return value % 5 == 0 ? .4f : .3f;
        } 

        public void Dispose()
        {
            _surface?.Dispose();
            _background.Dispose();
            _foreground.Dispose();
            _font.Dispose();
        }


    }
}
