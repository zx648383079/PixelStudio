using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.Drawing
{
    public class PathBuilder : IDisposable
    {

        private SKPath? _cachedPath = null;
        private bool _isDirty = true;
        public Point Begin { get; private set; } = new();
        public Point End { get; private set; } = new();

        public List<PathSegment> Items = [];

        public Rect Bounds => Build().Bounds.ToRect();
        public bool IsEmpty => Items.Count == 0;

        public int Count => Items.Count;

        #region 操作已有的

        public IEnumerable<int> PointIndexs => Items.Select((item, i) => item.Points.Count > 0 ? i : -1).Where(i => i >= 0);

        public int IndexOf(Point point)
        {
            for (int i = Items.Count - 1; i >= 0; i--)
            {
                var item = Items[i];
                if (item.Points.Count > 0 && item.Points.Last() == point)
                {
                    return i;
                }
            }
            return -1;
        }
        public Point? GetPoint(int index)
        {
            if (index < 0 || index >= Items.Count)
            {
                return null;
            }
            var item = Items[index];
            if (item.Points.Count == 0)
            {
                return null;
            }
            return item.Points.Last();
        }

        /// <summary>
        /// 移动到
        /// </summary>
        /// <param name="index"></param>
        /// <param name="to"></param>
        public void MoveTo(int index, Point to)
        {
            var item = Items[index];
            if (item.Points.Count == 0)
            {
                return;
            }
            item.Points[^1] = to;
            _isDirty = true;
        }
        /// <summary>
        /// 移动指定距离
        /// </summary>
        /// <param name="v"></param>
        /// <param name="offset"></param>
        public void Move(int index, Vector2 offset)
        {
            var item = Items[index];
            if (item.Points.Count == 0)
            {
                return;
            }
            item.Points[^1] += offset;
            _isDirty = true;
        }

        public void Remove(int index)
        {
            Items.RemoveAt(index);
            _isDirty = true;
        }
        #endregion


        #region 新增


        public PathBuilder MoveTo(float x, float y)
        {
            return MoveTo(new Point(x, y));
        }

        public PathBuilder MoveTo(Point point)
        {
            if (Items.Count == 0)
            {
                Begin = point;
            }
            Items.Add(new(SKPathVerb.Move, point));
            End = point;
            _isDirty = true;
            return this;
        }

        public PathBuilder LineTo(float x, float y)
        {
            return LineTo(new Point(x, y));
        }

        public PathBuilder LineTo(Point point)
        {
            if (point == End)
            {
                return this;
            }
            Items.Add(new(SKPathVerb.Line, point));
            End = point;
            _isDirty = true;
            return this;
        }
        /// <summary>
        /// 圆滑曲线
        /// </summary>
        /// <param name="ctrlX"></param>
        /// <param name="ctrlY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <returns></returns>
        public PathBuilder QuadTo(float ctrlX, float ctrlY, float endX, float endY)
        {
            return QuadTo(new Point(ctrlX, ctrlY), new Point(endX, endY));
        }
        /// <summary>
        /// 圆滑曲线
        /// </summary>
        /// <param name="controlPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public PathBuilder QuadTo(Point controlPoint, Point endPoint)
        {
            Items.Add(new(SKPathVerb.Quad, controlPoint, endPoint));
            End = endPoint;
            _isDirty = true;
            return this;
        }
        
        public PathBuilder CubicTo(float ctrl1X, float ctrl1Y, float ctrl2X, float ctrl2Y, float endX, float endY)
        {
            return CubicTo(
                new Point(ctrl1X, ctrl1Y),
                new Point(ctrl2X, ctrl2Y),
                new Point(endX, endY));
        }
        /// <summary>
        /// 贝塞尔曲线
        /// </summary>
        /// <param name="controlPoint1"></param>
        /// <param name="controlPoint2"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public PathBuilder CubicTo(Point controlPoint1, Point controlPoint2, Point endPoint)
        {
            Items.Add(new(SKPathVerb.Cubic, controlPoint1, controlPoint2, endPoint));
            End = endPoint;
            _isDirty = true;
            return this;
        }
        /// <summary>
        /// 绘制弧线（实际是截取圆或椭圆的一部分）
        /// </summary>
        /// <param name="rect">椭圆的外接矩形</param>
        /// <param name="startAngle">起始角度（度数，0度=右侧水平方向）</param>
        /// <param name="sweepAngle">扫描角度（度数，正数为逆时针，负数为顺时针）</param>
        /// <returns></returns>
        public PathBuilder ArcTo(Rect rect, float startAngle, float sweepAngle)
        {
            if (Math.Abs(sweepAngle) < 0.001f)
            {
                return this;
            }

            // 将角度转换为弧度
            var startRad = (float)(startAngle * Math.PI / 180);
            var sweepRad = (float)(sweepAngle * Math.PI / 180);

            // 椭圆参数
            var rx = rect.Width / 2;
            var ry = rect.Height / 2;
            var center = rect.Center;

            // 计算需要分成多少段（每段最大90度以保证精度）
            var segments = (int)Math.Ceiling(Math.Abs(sweepAngle) / 90.0f);
            var segmentSweep = sweepAngle / segments;
            var segmentSweepRad = (float)(segmentSweep * Math.PI / 180);

            // 预计算贝塞尔曲线的魔术常数（用于近似圆弧）
            var k = (float)(4.0 / 3.0 * Math.Tan(segmentSweepRad / 4));

            var currentAngle = startRad;

            for (var i = 0; i < segments; i++)
            {
                var cosStart = (float)Math.Cos(currentAngle);
                var sinStart = (float)Math.Sin(currentAngle);
                var cosEnd = (float)Math.Cos(currentAngle + segmentSweepRad);
                var sinEnd = (float)Math.Sin(currentAngle + segmentSweepRad);

                // 起点
                var p0 = new Point(center.X + rx * cosStart, center.Y + ry * sinStart);
                // 终点
                var p3 = new Point(center.X + rx * cosEnd, center.Y + ry * sinEnd);

                // 控制点1（沿切线方向延伸）
                var p1 = new Point(
                    p0.X + k * rx * -sinStart,
                    p0.Y + k * ry * cosStart
                );

                // 控制点2（从终点沿反切线方向延伸）
                var p2 = new Point(
                    p3.X + k * rx * sinEnd,
                    p3.Y + k * ry * -cosEnd
                );

                // 添加三次贝塞尔曲线
                CubicTo(p1, p2, p3);

                currentAngle += segmentSweepRad;
            }
            return this;
        }

        public PathBuilder Close()
        {
            Items.Add(new(SKPathVerb.Close));
            End = Begin;
            _isDirty = true;
            return this;
        }
        public PathBuilder AddRect(Point point, Size size)
        {
            return AddRect(new Rect(point, size));
        }

        public PathBuilder AddRect(Size size)
        {
            return AddRect(new Rect(End, size));
        }
        public PathBuilder AddRect(Rect rect)
        {
            return MoveTo(rect.Left, rect.Top)
                .LineTo(rect.Right, rect.Top)
                .LineTo(rect.Right, rect.Bottom)
                .LineTo(rect.Left, rect.Bottom)
                .LineTo(rect.Left, rect.Top);
        }

        public PathBuilder AddRect(RoundRect rect)
        {
            return AddRect(new Rect(rect.X, rect.Y, rect.Width, rect.Height), 
                rect.LeftTopRadius, rect.RightTopRadius, rect.RightBottomRadius, rect.LeftBottomRadius);
        }
        public PathBuilder AddRect(Point point, Size size,
            float radius)
        {
            return AddRect(point, size, radius, radius, radius, radius);
        }
        public PathBuilder AddRect(Point point, Size size,
            float xRadius, float yRadius)
        {
            var radius = new Vector2(xRadius, yRadius);
            return AddRect(new Rect(point, size), radius, radius, radius, radius);
        }
        public PathBuilder AddRect(Point point, Size size, 
            float leftTopRadius, float rightTopRadius,
            float rightBottomRadius, float leftBottomRadius)
        {
            return AddRect(new Rect(point, size), new Vector2(leftTopRadius, leftTopRadius),
                new Vector2(rightTopRadius, rightTopRadius),
                new Vector2(rightBottomRadius, rightBottomRadius),
                new Vector2(leftBottomRadius, leftBottomRadius));
        }

        public PathBuilder AddRect(Rect rect,
            Vector2 leftTopRadius, Vector2 rightTopRadius,
            Vector2 rightBottomRadius, Vector2 leftBottomRadius)
        {
            var isValid = RoundRect.IsRadius(leftTopRadius);
            var start = new Point(rect.Left, rect.Top + (isValid ? leftTopRadius.Y : 0));
            MoveTo(start);
            if (isValid)
            {
                QuadTo(new Point(rect.Left, rect.Top), new Point(rect.Left + leftTopRadius.X, rect.Top));
            }
            isValid = RoundRect.IsRadius(rightTopRadius);
            LineTo(rect.Right - (isValid ? rightTopRadius.X : 0), rect.Top);
            if (isValid)
            {
                QuadTo(new Point(rect.Right, rect.Top), new Point(rect.Right, rect.Top + rightTopRadius.Y));
            }
            isValid = RoundRect.IsRadius(rightBottomRadius);
            LineTo(rect.Right, rect.Bottom - (isValid ? rightBottomRadius.Y : 0));
            if (isValid)
            {
                QuadTo(new Point(rect.Right, rect.Bottom), new Point(rect.Right - rightBottomRadius.X, rect.Bottom));
            }

            isValid = RoundRect.IsRadius(leftBottomRadius);
            LineTo(rect.Left + (isValid ? leftBottomRadius.X : 0), rect.Bottom);
            if (isValid)
            {
                QuadTo(new Point(rect.Left, rect.Bottom), new Point(rect.Left, rect.Bottom - leftBottomRadius.Y));
            }
            LineTo(start);
            return this;
        }
        

        public PathBuilder AddCircle(Point center, float radius)
        {
            return AddEllipse(new Rect(
                center.X - radius,
                center.Y - radius,
                center.X + radius,
                center.Y + radius
            ));
        }

        public PathBuilder AddOval(Point center, Size radius)
        {
            var xRadius = radius.Width / 2;
            var yRadius = radius.Height / 2;
            return AddEllipse(new Rect(
                center.X - xRadius,
                center.Y - yRadius,
                center.X + xRadius,
                center.Y + yRadius
            ));
        }

        public PathBuilder AddEllipse(Rect rect)
        {
            // 椭圆参数
            float rx = rect.Width / 2;   // x轴半径
            float ry = rect.Height / 2;  // y轴半径
            var center = rect.Center;

            // 贝塞尔曲线拟合圆的魔术常数
            // 对于单位圆，控制点距离为 4/3 * tan(π/8) ≈ 0.5522847498
            const float k = 0.5522847498f;

            var kx = k * rx;
            var ky = k * ry;

            // 定义4个关键点（0°, 90°, 180°, 270°）
            var start = new Point(center.X + rx, center.Y);           // 0° (右侧)


            // 绘制4段三次贝塞尔曲线
            return MoveTo(start)
                .CubicTo(new Point(center.X + rx, center.Y + ky), 
                    new Point(center.X + kx, center.Y + ry), 
                    new Point(center.X, center.Y + ry))   // 第1段：0° -> 90°
                .CubicTo(new Point(center.X - kx, center.Y + ry), 
                    new Point(center.X - rx, center.Y + ky), 
                    new Point(center.X - rx, center.Y))   // 第2段：90° -> 180°
                .CubicTo(new Point(center.X - rx, center.Y - ky), 
                    new Point(center.X - kx, center.Y - ry), 
                    new Point(center.X, center.Y - ry))   // 第3段：180° -> 270°
                .CubicTo(new Point(center.X + kx, center.Y - ry), 
                    new Point(center.X + rx, center.Y - ky), start);   // 第4段：270° -> 360°（闭合）;
        }
        #endregion

        #region 与 SKPath 转换

        public SKPath Build()
        {
            if (!_isDirty && _cachedPath != null)
            {
                return _cachedPath;
            }

            _cachedPath?.Dispose();
            _cachedPath = new SKPath();

            foreach (var cmd in Items)
            {
                switch (cmd.Type)
                {
                    case SKPathVerb.Move:
                        if (cmd.Points.Count >= 1)
                        {
                            _cachedPath.MoveTo(cmd.Points[0].ToPoint());
                        }
                        break;
                    case SKPathVerb.Line:
                        if (cmd.Points.Count >= 1)
                        {
                            _cachedPath.LineTo(cmd.Points[0].ToPoint());
                        }
                        break;
                    case SKPathVerb.Quad:
                        if (cmd.Points.Count >= 2)
                        {
                            _cachedPath.QuadTo(cmd.Points[0].ToPoint(), cmd.Points[1].ToPoint());
                        }
                        break;
                    case SKPathVerb.Cubic:
                        if (cmd.Points.Count >= 3)
                        {
                            _cachedPath.CubicTo(cmd.Points[0].ToPoint(), 
                                cmd.Points[1].ToPoint(), cmd.Points[2].ToPoint());
                        }
                        break;
                    case SKPathVerb.Close:
                        _cachedPath.Close();
                        break;
                }
            }

            _isDirty = false;
            return _cachedPath;
        }

        public static PathBuilder FromPath(SKPath path)
        {
            var builder = new PathBuilder();
            // 创建路径迭代器
            using (var iterator = path.CreateIterator(false))
            {
                var points = new SKPoint[4];

                // 遍历所有命令
                while (true)
                {
                    // 获取下一个动词
                    var verb = iterator.Next(points);

                    if (verb == SKPathVerb.Done)
                    {
                        break;
                    }

                    switch (verb)
                    {
                        case SKPathVerb.Move:
                            builder.MoveTo(points[0].ToPoint());
                            break;
                        case SKPathVerb.Line:
                            builder.LineTo(points[0].ToPoint());
                            break;
                        case SKPathVerb.Quad:
                            builder.QuadTo(points[0].ToPoint(), points[1].ToPoint());
                            break;
                        case SKPathVerb.Cubic:
                            builder.CubicTo(points[0].ToPoint(), points[1].ToPoint(), points[2].ToPoint());
                            break;
                        case SKPathVerb.Conic:
                            // 圆锥曲线（带权重），可用 QuadTo 近似或存储权重
                            builder.QuadTo(points[0].ToPoint(), points[1].ToPoint());
                            break;
                        case SKPathVerb.Close:
                            builder.Close();
                            break;
                    }
                }
            }
            builder._cachedPath = path;
            builder._isDirty = false;
            return builder;
        }
        #endregion
        #region 与 svg 转换

        public static PathBuilder FromSvgPath(string text)
        {
            return FromPath(SKPath.ParseSvgPathData(text));
        }

        public string ToSvgPath()
        {
            return Build().ToSvgPathData();
        }

        #endregion

        public void Dispose()
        {
            Items.Clear();
            _cachedPath?.Dispose();
        }
    }
}
