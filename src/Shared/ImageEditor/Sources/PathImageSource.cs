using SkiaSharp;
using System;
using System.Numerics;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor.Sources
{
    public class PathImageSource(IImageEditor editor) : BaseImageSource(editor)
    {
        public PathImageSource(SKPath path, IImageEditor editor)
            : this(editor)
        {
            _path = PathBuilder.FromPath(path);
        }

        public PathImageSource(PathBuilder path, IImageEditor editor) : this(editor)
        {
            _path = path;
        }

        private readonly SKPaint _paint = new()
        {
            Color = SKColors.Black,
            ColorF = SKColors.Black,
            StrokeWidth = 2,
            IsStroke = true,
            IsAntialias = true,
        };
        private readonly BitmapBuilder _thumbnail = new();

        private readonly PathBuilder _path = new();

        public SKColor FillColor { get; set; }
        public SKColor StrokeColor { get; set; }

        public float StrokeWidth { get; set; }
        public override Rect Bound => _path.Bounds;

        public PathBuilder Source => _path;


        public override object? CreateThumbnail(Size size)
        {
            return _thumbnail.Mutate(size, canvas => 
            {
                (canvas as ISKImageCanvas)?.DrawPath(_path.Build(), _paint);
            });
        }

        public void Add(Point point)
        {
            if (_path.IsEmpty)
            {
                _path.MoveTo(point);
                return;
            }
            _path.LineTo(point);
        }

        public void ClosePath()
        {
            _path.Close();
            _paint.IsStroke = false;
        }

        public int NearOf(Point point, float maxOffset)
        {
            for (int i = _path.Count - 1; i >= 0; i--)
            {
                var p = _path.GetPoint(i);
                if (p is null)
                {
                    continue;
                }
                var offset = point - p.Value;
                if (Math.Abs(offset.X) < maxOffset && Math.Abs(offset.Y) < maxOffset)
                {
                    return i;
                }
            }
            return -1;
        }

        public int IndexOf(Point point)
        {
            return _path.IndexOf(point);
        }


        public void RemoveAt(int index)
        {
            _path.Remove(index);
        }
        /// <summary>
        /// 移动到
        /// </summary>
        /// <param name="index"></param>
        /// <param name="to"></param>
        public void MoveTo(int index, Point to)
        {
            _path.MoveTo(index, to);
        }
        /// <summary>
        /// 移动指定距离
        /// </summary>
        /// <param name="v"></param>
        /// <param name="offset"></param>
        public void Move(int index, Vector2 offset)
        {
            _path.Move(index, offset);
        }

        public void Move(int[] indexs, Vector2 offset)
        {
            foreach (var item in indexs)
            {
                _path.Move(item, offset);
            }
        }

        public override void Paint(IImageCanvas canvas)
        {
            (canvas as ISKImageCanvas)?.DrawPath(_path.Build(), _paint);
        }

        public override void Paint(IImageStyleCanvas canvas, IImageStyle computedStyle)
        {
            (canvas as ISKImageCanvas)?.DrawPath(_path.Build(), _paint);
        }

        public override void Dispose()
        {
            base.Dispose();
            _paint.Dispose();
            _path.Dispose();
        }


    }
}
