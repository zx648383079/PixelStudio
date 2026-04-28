using SkiaSharp;
using Svg.Skia;
using System;

namespace ZoDream.Shared.Drawing
{
    public class SvgFileImageData(string fileName) : IImageData
    {

        public string FileName => fileName;

        public SKBitmap? ToBitmap()
        {
            var svg = new SKSvg();
            svg.Load(fileName);
            return svg.Picture?.ToBitmap(SKColors.Transparent, 1, 1,
                SKColorType.Rgba8888, SKAlphaType.Premul, null);
        }

        public SKImage? ToImage()
        {
            throw new NotImplementedException();
        }
    }
}
