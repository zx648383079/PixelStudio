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
            DrawSurface(surface, new SKPoint(style.X, style.Y));
        }

        public IImageStyle Compute(IImageLayer layer)
        {
            return styler.Compute(layer);
        }

        public void DrawBitmap(SKBitmap? source, float x, float y)
        {
            if (source is null)
            {
                return;
            }
            canvas.DrawBitmap(source, x + X, y + Y);
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
        public void DrawBitmap(SKBitmap source)
        {
            canvas.DrawBitmap(source, SKPoint.Empty);
        }

        public void DrawBitmap(SKBitmap source, SKPoint point)
        {
            DrawBitmap(source, SKRect.Create(point, new SKSize(source.Width, source.Height)));
        }

        public void DrawBitmap(SKBitmap source, SKRect rect)
        {
            canvas.DrawBitmap(source, rect);
        }

        public void DrawBitmap(SKBitmap source, SKRect rect, SKPaint paint)
        {
            canvas.DrawBitmap(source, rect, paint);
        }

        public void DrawCircle(SKPoint center, float radius, SKPaint paint)
        {
            canvas.DrawCircle(center, radius, paint);
        }

        public void DrawOval(SKPoint center, SKSize radius, SKPaint paint)
        {
            canvas.DrawOval(center, radius, paint);
        }

        public void DrawPath(SKPath path, SKPaint paint)
        {
            canvas.DrawPath(path, paint);
        }

        public void DrawLine(SKPoint from, SKPoint to, SKPaint paint)
        {
            canvas.DrawLine(from, to, paint);
        }

        public void DrawRect(SKRect rect, SKPaint paint)
        {
            canvas.DrawRect(rect, paint);
        }

        public void DrawRect(SKRoundRect rect, SKPaint paint)
        {
            canvas.DrawRoundRect(rect, paint);
        }

        public void DrawSurface(SKSurface surface)
        {
            DrawSurface(surface, SKPoint.Empty);
        }

        public void DrawSurface(SKSurface surface, SKPoint point)
        {
            canvas.DrawSurface(surface, point);
        }

        public void DrawSurface(SKSurface surface, SKRect rect)
        {
            canvas.DrawSurface(surface, rect.Left, rect.Top);
        }

        public void DrawPicture(SKPicture picture, SKPoint point)
        {
            canvas.DrawPicture(picture, point);
        }
        public void DrawPicture(SKPicture picture, SKRect rect)
        {
            DrawPicture(picture, rect, null);
        }
        public void DrawPicture(SKPicture picture, SKRect rect, SKPaint paint) 
        {
            var matrix = SKMatrix.CreateScaleTranslation(rect.Width / picture.CullRect.Width,
                rect.Height / picture.CullRect.Height,
                rect.Left,
                rect.Top);
            canvas.DrawPicture(picture, matrix, paint);
        }

        public void DrawText(string text, SKPoint point, SKTextAlign textAlign, SKFont font, SKPaint paint)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }
            canvas.DrawText(text, point, textAlign, font, paint);
        }

        public void DrawTexture(SKBitmap source, SKPoint[] sourceVertices, SKPoint[] vertices)
        {
            using var paint = new SKPaint()
            {
                IsAntialias = true,
                Shader = SKShader.CreateBitmap(source, SKShaderTileMode.Clamp, SKShaderTileMode.Clamp)
            };
            canvas.DrawVertices(SKVertexMode.TriangleFan,
                vertices,
                sourceVertices, null, paint);
        }


        public void DrawBitmap(SKBitmap source, IImageStyle style)
        {
            if (source is null)
            {
                return;
            }
            if (style is IImageComputedVertexStyle u)
            {
                DrawTexture(source, u.SourceItems.Select(i => i.ToPoint()).ToArray(), u.PointItems.Select(i => i.ToPoint()).ToArray());
                return;
            }
            if (style is IImageVertexStyle v)
            {
                DrawTexture(source, EditorExtension.ComputeVertex(v.VertexItems, style)
                    .Select(i => i.ToPoint()).ToArray(), v.PointItems.Select(i => i.ToPoint()).ToArray());
                return;
            }
            DrawBitmap(source, style.X, style.Y);
        }

        public void DrawSurface(SKSurface surface, IImageStyle style)
        {
            if (surface is null)
            {
                return;
            }
            DrawSurface(surface, new SKPoint(style.X, style.Y));
        }

        public void DrawPicture(SKPicture picture, IImageStyle style)
        {
            if (picture is null)
            {
                return;
            }
            DrawPicture(picture, new SKPoint(style.X, style.Y));
        }

        public void DrawText(string text, IImageStyle style, SKTextAlign textAlign, SKFont font, SKPaint paint)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }
            DrawText(text, new SKPoint(style.X, style.Y), textAlign, font, paint);
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
                p.Paint(canvas, point.ToPoint());
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
                p.Paint(canvas, rect.ToRect(), (paint as ImagePaint)?.Source);
            }
        }

        public void Draw(IImageBuffer source, IImagePaint paint)
        {
            if (source is ISKImagePixel p)
            {
                p.Paint(canvas, SKPoint.Empty, (paint as ImagePaint)?.Source);
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
                p.Paint(canvas, SKPoint.Empty, (paint as ImagePaint)?.Source);
            }
        }

        public void DrawLine(Point from, Point to, IImagePaint paint)
        {
            canvas.DrawLine(from.ToPoint(), to.ToPoint(), (paint as ImagePaint)?.Source);
        }

        public void DrawRect(Rect rect, IImagePaint paint)
        {
            canvas.DrawRect(rect.ToRect(), (paint as ImagePaint)?.Source);
        }

        public void DrawRect(RoundRect rect, IImagePaint paint)
        {
            canvas.DrawRoundRect(rect.ToRect(), (paint as ImagePaint)?.Source);
        }

        public void DrawCircle(Point center, float radius, IImagePaint paint)
        {
            canvas.DrawCircle(center.ToPoint(), radius, (paint as ImagePaint)?.Source);
        }


        public void DrawOval(Point center, Vector2 radius, IImagePaint paint)
        {
            canvas.DrawOval(center.X, center.Y, radius.X, radius.Y, (paint as ImagePaint)?.Source);
        }

        public void Draw(string text, Point point, IImagePaint paint)
        {
            if (paint is FontImagePaint p)
            {
                p.Paint(canvas, text, point.ToPoint());
            }
        }

        public void Draw(IImagePixel source, Point[] sourceVertices, Point[] vertices)
        {
            if (source is ISKImagePixel p)
            {
                p.Paint(canvas, sourceVertices.Select(i => i.ToPoint()).ToArray(), vertices.Select(i => i.ToPoint()).ToArray());
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
                Draw(source, EditorExtension.ComputeVertex(v.VertexItems, style), v.PointItems);
                return;
            }
            Draw(source, new Point(style.X, style.Y));
        }

        public void Draw(string text, IImageStyle style, IImagePaint paint)
        {
            Draw(text, new Point(style.X, style.Y), paint);
        }

        public void Dispose()
        {
        }


    }
}
