using SkiaSharp;
using System;
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
                        EditorExtension.ConvertTo(v.PointItems));
                }
                return;
            }
            Draw(source, new Point(style.X, style.Y));
        }

        public virtual void Draw(string text, IImageStyle style, IImagePaint paint)
        {
            Draw(text, new Point(style.X, style.Y), paint);
        }

        #region sk

        public void Draw(SKBitmap source, SKPoint point)
        {
            canvas.DrawBitmap(source, point);
        }
        public void Draw(SKBitmap source, SKRect rect, SKPaint paint)
        {
            canvas.DrawBitmap(source, rect, paint);
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
            canvas.DrawImage(source, point);
        }
        public void Draw(SKImage source, SKRect rect, SKPaint paint)
        {
            canvas.DrawImage(source, rect, paint);
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
            canvas.DrawSurface(source, point, paint);
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
            canvas.DrawPicture(source, point);
        }
        public void Draw(SKPicture source, SKRect rect, SKPaint paint)
        {
            var matrix = SKMatrix.CreateScaleTranslation(rect.Width / source.CullRect.Width,
                rect.Height / source.CullRect.Height,
                rect.Left,
                rect.Top);
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

        public void DrawText(string text, SKPoint point, SKTextAlign textAlign, SKFont font, SKPaint paint)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }
            canvas.DrawText(text, point, textAlign, font, paint);
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
                vertices,
                sourceVertices, null, paint);
        }


        #endregion

        public void Dispose()
        {
            canvas.Dispose();
        }
    }
}
