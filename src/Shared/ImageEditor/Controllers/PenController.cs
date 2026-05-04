using SkiaSharp;
using System;
using ZoDream.Shared.ImageEditor.Sources;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor.Controllers
{
    /// <summary>
    /// 钢笔工具，绘制路径
    /// </summary>
    /// <param name="editor"></param>
    public class PenController(IImageEditor editor) : ICommandController, IMouseState
    {
        public bool IsEnabled => true;
        private bool _isRightButtonPressed = false;
        private Point _last = new();
        private readonly SKPaint _paint = new()
        {
            Color = SKColors.Blue.WithAlpha(150),
            StrokeWidth = 1,
            IsStroke = true
        };
        private PathImageSource? _layer;


        public void Initialize(IImageLayer? layer)
        {
            if (layer?.Source is PathImageSource p)
            {
                _layer = p;
                return;
            }
            _layer = null;
        }

        public void PointerMoved(IMouseRoutedArgs args)
        {
            var p = args.Position;
            if (!args.IsShiftPressed || _layer?.Source.IsEmpty != false)
            {
                _last = p;
            } else
            {
                var offset = p - _layer.Source.End;
                if (Math.Abs(offset.X) >= Math.Abs(offset.Y))
                {
                    _last = new Point(p.X, _layer.Source.End.Y);
                }
                else
                {
                    _last = new Point(_layer.Source.End.X, p.Y);
                }
            }

            if (_layer is null || _layer.Source.IsEmpty)
            {
                return;
            }
            editor.Invalidate();
        }

        public void PointerPressed(IMouseRoutedArgs args)
        {
            _isRightButtonPressed = args.IsRightButtonPressed;
            _last = args.Position;
        }

        public void PointerReleased(IMouseRoutedArgs args)
        {
            if (_isRightButtonPressed)
            {
                _layer = null;
                editor.Invalidate();
                return;
            }
            if (_layer is null)
            {
                editor.Layer.Add(_layer = new PathImageSource(editor));
            }
            if (IsClosePath(_last))
            {
                _layer.ClosePath();
                _layer = null;
            } else
            {
                _layer.Add(_last);
            }
            editor.Invalidate();
        }

        private bool IsClosePath(Point point)
        {
            if (_layer!.Source.Count <= 2)
            {
                return false;
            }
            var offset = point - _layer.Source.Begin;
            var maxOffset = editor.Options.JointSize / 2;
            return Math.Abs(offset.X) < maxOffset && Math.Abs(offset.Y) < maxOffset;
        }


        public void Paint(IImageCanvas canvas)
        {
            if (_layer is null || _layer.Source.IsEmpty)
            {
                return;
            }
            canvas.DrawLine(_layer.Source.End, _last, new ImagePaint(_paint));
        }

        public void Dispose()
        {
            _paint?.Dispose();
        }

    }
}
