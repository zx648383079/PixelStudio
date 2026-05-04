using SkiaSharp;
using System;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor
{
    public class ImagePaint(SKPaint paint) : IImagePaint
    {

        public SKColor Color {
            set {
                paint.Color = value;
            }
        }

        public SKPaint Source => paint;

        public void Mutate(Action<SKPaint> cb)
        {
            cb.Invoke(paint);
        }

        public virtual void Dispose()
        {
            paint.Dispose();
        }

        public static ImagePaint CreateBorder(Color color, float strokeWidth = 1)
        {
            return new ImagePaint(new SKPaint()
            {
                Color = color.ToColor(),
                StrokeWidth = strokeWidth,
                Style = SKPaintStyle.Stroke,
            });
        }

        public static ImagePaint CreateDashedBorder(Color color, float strokeWidth = 1)
        {
            return new ImagePaint(new SKPaint()
            {
                Color = color.ToColor(),
                StrokeWidth = strokeWidth,
                Style = SKPaintStyle.Stroke,
                // [虚线长度, 间隙长度]
                PathEffect = SKPathEffect.CreateDash([10, 5], 0),
                IsAntialias = true
            });
        }

        public static ImagePaint CreateFill(Color fill, Color border, float strokeWidth = 1)
        {
            return new ImagePaint(new SKPaint()
            {
                Color = border.ToColor(),
                ColorF = fill.ToColor(),
                StrokeWidth = strokeWidth,
                Style = SKPaintStyle.StrokeAndFill,
            });
        }

        public static ImagePaint CreateFill(Color fill)
        {
            return new ImagePaint(new SKPaint()
            {
                ColorF = fill.ToColor(),
                Style = SKPaintStyle.Fill,
            });
        }

        public static ImagePaint CreateOpacity(float opacity)
        {
            return new ImagePaint(new SKPaint()
            {
                Color = SKColors.White.WithAlpha((byte)(255 * opacity)),
                IsAntialias = true
            });
        }
        /// <summary>
        /// 和颜色混合
        /// </summary>
        /// <param name="color"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static ImagePaint CreateBlend(Color color, SKBlendMode mode)
        {
            return new ImagePaint(new SKPaint()
            {
                Color = color.ToColor(),
                BlendMode = mode,
            });
        }
        /// <summary>
        /// 擦除功能
        /// </summary>
        /// <param name="strength"></param>
        /// <returns></returns>
        public static ImagePaint CreateErase(float strength = 1)
        {
            var paint = new SKPaint
            {
                Color = SKColors.Transparent,  // 完全透明
                BlendMode = SKBlendMode.Src,   // 直接替换像素
                IsAntialias = true
            };
            if (strength < 1f)
            {
                byte alpha = (byte)(255 * (1 - strength));
                paint.Color = new SKColor(0, 0, 0, alpha);
            }
            return new ImagePaint(paint);
        }
    }

    public class FontImagePaint(SKFont font, SKTextAlign align,  SKPaint paint) : ImagePaint(paint)
    {

        public void Paint(SKCanvas canvas, string text, SKPoint point)
        {
            canvas.DrawText(text, point, align, font, paint);
        }

        public override void Dispose()
        {
            base.Dispose();
            font.Dispose();
        }
    }
}
