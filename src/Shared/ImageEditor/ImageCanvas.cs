using SkiaSharp;
using System;
using System.IO;
using System.Linq;
using System.Numerics;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor
{
    public class ImageCanvas(SKCanvas canvas) : IImageStyleCanvas, ISKImageCanvas
    {
        /// <summary>
        /// 以透明背景
        /// </summary>
        public void Clear()
        {
            canvas.Clear(SKColors.Transparent);
        }
        /// <summary>
        /// 以指定颜色作为背景
        /// </summary>
        /// <param name="color"></param>
        public void Clear(Color color)
        {
            canvas.Clear(color.ToColor());
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


        public virtual void Mutate(IImageStyle style, Action<IImageStyleCanvas> cb)
        {
        }

        public virtual IImageStyle Compute(IImageLayer layer)
        {
            throw new NotImplementedException();
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


        public virtual IImageStyleCanvas Transform(Vector2 offset)
        {
            return this;
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

        public void DrawOval(Point center, Vector2 radius, IImagePaint paint)
        {
            canvas.DrawOval(center.X, center.Y, radius.X, radius.Y, (paint as ImagePaint)?.Source);
        }

        public virtual void Draw(IImagePixel source, IImageStyle style)
        {
        }

        public virtual void Draw(string text, IImageStyle style, IImagePaint paint)
        {
        }

        public void DrawBitmap(SKBitmap source, IImageStyle style)
        {
        }

        public void DrawPicture(SKPicture picture, IImageStyle style)
        {
        }

        public void Dispose()
        {
            canvas.Dispose();
        }
    }
}
