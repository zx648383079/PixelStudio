using SkiaSharp;
using System;
using System.Linq;
using System.Numerics;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor
{
    public class ImageStyleCanvas(SKCanvas canvas, IImageStyler styler): IImageStyleCanvas, ISKImageCanvas
    {
        public float X { get; set; }
        public float Y { get; set; }

        public IImageStyleCanvas Transform(float x, float y)
        {
            if (x == 0 && y == 0)
            {
                return this;
            }
            return new ImageStyleCanvas(canvas, styler)
            {
                X = X + x,
                Y = Y + y,
            };
        }

        public void Mutate(IImageStyle style, Action<IImageStyleCanvas> cb)
        {
            if (style.Rotate == 0)
            {
                cb(this);
                return;
            }
            var width = style.Width;
            var height = style.Height;
            if (style is IImageComputedStyle s)
            {
                width = s.ActualWidth;
                height = s.ActualHeight;
            }
            else
            {
                (width, height) =
                EditorExtension.ComputedRotate((int)(style.Width * Math.Abs(style.ScaleX)),
                (int)(style.Height * Math.Abs(style.ScaleY)),
                style.Rotate);
            }
            if (width == 0 || height == 0)
            {
                return;
            }
            var info = new SKImageInfo((int)width, (int)height);
            using var surface = SKSurface.Create(info);
            var c = surface.Canvas;
            c.Translate(width / 2, height / 2);
            c.RotateDegrees(style.Rotate);
            c.Scale(style.ScaleX, style.ScaleY);
            c.Translate(-style.Width / 2, -style.Height / 2);
            cb.Invoke(new ImageStyleCanvas(c, styler)
            {
                X = -style.X,
                Y = -style.Y
            });
            Draw(surface, new SKPoint(style.X, style.Y), null);
        }

        public IImageStyle Compute(IImageLayer layer)
        {
            return styler.Compute(layer);
        }

        private void Mutate(IImageStyle style, Action<SKCanvas, SKPoint> cb)
        {
            var point = new SKPoint(style.X + X, style.Y + Y);
            if (style.Rotate == 0)
            {
                cb(canvas, point);
                return;
            }
            var width = style.Width;
            var height = style.Height;
            if (style is IImageComputedStyle s)
            {
                width = s.ActualWidth;
                height = s.ActualHeight;
            }
            else
            {
                (width, height) =
                EditorExtension.ComputedRotate(style.Width, style.Height,
                style.Rotate);
            }
            var info = new SKImageInfo((int)width, (int)height);
            using var surface = SKSurface.Create(info);
            var c = surface.Canvas;
            c.Translate(style.Width / 2, style.Height / 2);
            c.RotateDegrees(style.Rotate);
            c.Translate(-width / 2, -height / 2);
            cb(c, new SKPoint(0, 0));
            canvas.DrawSurface(surface, point);
        }
        



        public void Clear()
        {
            canvas.Clear(SKColors.Transparent);
        }

        public void Clear(Color color)
        {
            canvas.Clear(color.ToColor());
        }

        public void Draw(IImagePixel source)
        {
            Draw(source, new Point());
        }

        public void Draw(IImagePixel source, Point point)
        {
            if (source is ISKImagePixel p)
            {
                p.Paint(canvas, new SKPoint(point.X + X, point.Y + Y));
            }
        }

        public void Draw(IImagePixel source, Rect rect)
        {
            Draw(source, rect, null);
        }

        public void Draw(IImagePixel source, Rect rect, IImagePaint paint)
        {
            if (source is ISKImagePixel p)
            {
                var r = rect.ToRect();
                r.Offset(X, Y);
                p.Paint(canvas, r, (paint as ImagePaint)?.Source);
            }
        }

        public void Draw(IImageBuffer source, IImagePaint paint)
        {
            if (source is ISKImagePixel p)
            {
                p.Paint(canvas, new SKPoint(X, Y), (paint as ImagePaint)?.Source);
            }
        }

        public void Draw(IImageBuffer source)
        {
            Draw(source, null);
        }


        public void Draw(IPathBuffer path, IImagePaint paint)
        {
            if (path is ISKImagePixel p)
            {
                p.Paint(canvas, new SKPoint(X, Y), (paint as ImagePaint)?.Source);
            }
        }

        public void DrawLine(Point from, Point to, IImagePaint paint)
        {
            canvas.DrawLine(new SKPoint(from.X + X, from.Y + Y), 
                new SKPoint(to.X + X, to.Y + Y), (paint as ImagePaint)?.Source);
        }

        public void DrawRect(Rect rect, IImagePaint paint)
        {
            var r = rect.ToRect();
            r.Offset(X, Y);
            canvas.DrawRect(r, (paint as ImagePaint)?.Source);
        }

        public void DrawRect(RoundRect rect, IImagePaint paint)
        {
            var r = rect.ToRect();
            r.Offset(X, Y);
            canvas.DrawRoundRect(r, (paint as ImagePaint)?.Source);
        }

        public void DrawCircle(Point center, float radius, IImagePaint paint)
        {
            canvas.DrawCircle(new SKPoint(center.X + X, center.Y + Y), radius, (paint as ImagePaint)?.Source);
        }


        public void DrawOval(Point center, Vector2 radius, IImagePaint paint)
        {
            canvas.DrawOval(center.X + X, center.Y+ Y, radius.X, radius.Y, (paint as ImagePaint)?.Source);
        }

        public void Draw(string text, Point point, IImagePaint paint)
        {
            if (paint is FontImagePaint p)
            {
                p.Paint(canvas, text, new SKPoint(point.X + X, point.Y + Y));
            }
        }

        public void Draw(IImagePixel source, Point[] sourceVertices, Point[] vertices)
        {
            if (source is ISKImagePixel p)
            {
                p.Paint(canvas, sourceVertices.Select(i => i.ToPoint()).ToArray(), 
                    vertices.Select(i => new SKPoint(i.X + X, i.Y + Y)).ToArray());
            }
        }

        public IImageStyleCanvas Transform(Vector2 offset)
        {
            return Transform(offset.X, offset.Y);
        }

        public void Draw(IImagePixel source, IImageStyle style)
        {
            if (style is IImageComputedVertexStyle u)
            {
                Draw(source, u.SourceItems, u.PointItems);
                return;
            }
            if (style is IImageVertexStyle v)
            {
                if (source is ISKImagePixel p)
                {
                    p.Paint(canvas, EditorExtension.ComputeVertex(v.VertexItems, style),
                        v.PointItems.Select(i => new SKPoint(i.X + X, i.Y + Y)).ToArray());
                }
                return;
            }
            Draw(source, new Point(style.X, style.Y));
        }

        public void Draw(string text, IImageStyle style, IImagePaint paint)
        {
            Draw(text, new Point(style.X + X, style.Y + Y), paint);
        }



        #region sk

        public void Draw(SKBitmap source, SKPoint point)
        {
            var offset = new SKPoint(X, Y);
            canvas.DrawBitmap(source, point + offset, SKSamplingOptions.Default);
        }
        public void Draw(SKBitmap source, SKRect rect, SKPaint paint)
        {
            rect.Offset(X, Y);
            canvas.DrawBitmap(source, rect, SKSamplingOptions.Default, paint);
        }

        public void Draw(SKBitmap source, IImageStyle style)
        {
            if (source is null)
            {
                return;
            }
            if (style is IImageComputedVertexStyle u)
            {
                Draw(SKShader.CreateBitmap(source, SKShaderTileMode.Clamp, SKShaderTileMode.Clamp),
                    EditorExtension.ConvertTo(u.SourceItems), EditorExtension.ConvertTo(u.PointItems));
                return;
            }
            if (style is IImageVertexStyle v)
            {
                Draw(SKShader.CreateBitmap(source, SKShaderTileMode.Clamp, SKShaderTileMode.Clamp),
                    EditorExtension.ComputeVertex(v.VertexItems, style), EditorExtension.ConvertTo(v.PointItems));
                return;
            }
            Draw(source, new SKPoint(style.X, style.Y));
        }

        public void Draw(SKImage source, SKPoint point)
        {
            var offset = new SKPoint(X, Y);
            canvas.DrawImage(source, point + offset, SKSamplingOptions.Default);
        }
        public void Draw(SKImage source, SKRect rect, SKPaint paint)
        {
            rect.Offset(X, Y);
            canvas.DrawImage(source, rect, SKSamplingOptions.Default, paint);
        }
        public void Draw(SKImage source, IImageStyle style)
        {
            if (source is null)
            {
                return;
            }
            if (style is IImageComputedVertexStyle u)
            {
                Draw(SKShader.CreateImage(source, SKShaderTileMode.Clamp, SKShaderTileMode.Clamp),
                    EditorExtension.ConvertTo(u.SourceItems), EditorExtension.ConvertTo(u.PointItems));
                return;
            }
            if (style is IImageVertexStyle v)
            {
                Draw(SKShader.CreateImage(source, SKShaderTileMode.Clamp, SKShaderTileMode.Clamp),
                    EditorExtension.ComputeVertex(v.VertexItems, style), EditorExtension.ConvertTo(v.PointItems));
                return;
            }
            Draw(source, new SKPoint(style.X, style.Y));
        }

        public void Draw(SKSurface source, SKPoint point, SKPaint paint)
        {
            var offset = new SKPoint(X, Y);
            canvas.DrawSurface(source, point + offset, paint);
        }

        public void Draw(SKSurface source, SKRect rect, SKPaint paint)
        {
            using var p = source.Snapshot();
            Draw(p, rect, paint);
        }

        public void Draw(SKSurface source, IImageStyle style)
        {
            if (source is null)
            {
                return;
            }
            Draw(source, new SKPoint(style.X, style.Y), null);
        }

        public void Draw(SKPicture source, SKPoint point)
        {
            var offset = new SKPoint(X, Y);
            canvas.DrawPicture(source, point + offset);
        }
        public void Draw(SKPicture source, SKRect rect, SKPaint paint)
        {
            var matrix = SKMatrix.CreateScaleTranslation(rect.Width / source.CullRect.Width,
                rect.Height / source.CullRect.Height,
                rect.Left + X,
                rect.Top + Y);
            canvas.DrawPicture(source, matrix, paint);
        }
        public void Draw(SKPicture source, IImageStyle style)
        {
            if (source is null)
            {
                return;
            }
            if (style is IImageComputedVertexStyle u)
            {
                Draw(SKShader.CreatePicture(source, SKShaderTileMode.Clamp, SKShaderTileMode.Clamp),
                    EditorExtension.ConvertTo(u.SourceItems), EditorExtension.ConvertTo(u.PointItems));
                return;
            }
            if (style is IImageVertexStyle v)
            {
                Draw(SKShader.CreatePicture(source, SKShaderTileMode.Clamp, SKShaderTileMode.Clamp),
                    EditorExtension.ComputeVertex(v.VertexItems, style), EditorExtension.ConvertTo(v.PointItems));
                return;
            }
            Draw(source, new SKPoint(style.X, style.Y));
        }

        public void DrawCircle(SKPoint center, float radius, SKPaint paint)
        {
            var offset = new SKPoint(X, Y);
            canvas.DrawCircle(center + offset, radius, paint);
        }

        public void DrawOval(SKPoint center, SKSize radius, SKPaint paint)
        {
            var offset = new SKPoint(X, Y);
            canvas.DrawOval(center + offset, radius, paint);
        }

        public void DrawPath(SKPath path, SKPaint paint)
        {
            path.Transform(SKMatrix.CreateTranslation(X, Y));
            canvas.DrawPath(path, paint);
        }

        public void DrawLine(SKPoint from, SKPoint to, SKPaint paint)
        {
            var offset = new SKPoint(X, Y);
            canvas.DrawLine(from + offset, to + offset, paint);
        }

        public void DrawRect(SKRect rect, SKPaint paint)
        {
            rect.Offset(X, Y);
            canvas.DrawRect(rect, paint);
        }

        public void DrawRect(SKRoundRect rect, SKPaint paint)
        {
            rect.Offset(X, Y);
            canvas.DrawRoundRect(rect, paint);
        }

        public void DrawText(string text, SKPoint point, SKTextAlign textAlign, SKFont font, SKPaint paint)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }
            canvas.DrawText(text, new SKPoint(point.X + X, point.Y + Y), textAlign, font, paint);
        }

        public void Draw(SKBitmap source, SKPoint[] sourceVertices, SKPoint[] vertices)
        {
            Draw(SKShader.CreateBitmap(source, SKShaderTileMode.Clamp, SKShaderTileMode.Clamp),
                sourceVertices, vertices);
        }

        public void Draw(SKShader source, SKPoint[] sourceVertices, SKPoint[] vertices)
        {
            using var paint = new SKPaint()
            {
                IsAntialias = true,
                Shader = source
            };
            canvas.DrawVertices(SKVertexMode.TriangleFan,
                vertices.Select(i => new SKPoint(i.X + X, i.Y + Y)).ToArray(),
                sourceVertices, null, paint);
        }


        #endregion


        public void Dispose()
        {
        }


    }
}
