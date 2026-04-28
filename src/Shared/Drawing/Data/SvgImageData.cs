using SkiaSharp;
using Svg.Skia;
using System;

namespace ZoDream.Shared.Drawing
{
    public class SvgImageData(string content) : IImageData
    {

        public string Content => content;

        public SKBitmap? ToBitmap()
        {
            var svg = SKSvg.CreateFromSvg(content);
            return svg.Picture?.ToBitmap(SKColors.Transparent, 1, 1,
                SKColorType.Rgba8888, SKAlphaType.Premul, null);
        }

        public SKImage? ToImage()
        {
            throw new NotImplementedException();
        }
    }
}
