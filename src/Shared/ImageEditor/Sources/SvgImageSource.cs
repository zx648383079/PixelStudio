using Svg.Skia;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor.Sources
{
    public class SvgImageSource : BaseImageSource
    {

        public SvgImageSource(SKSvg svg)
        {
            Source = svg;
            if (svg.Picture is null)
            {
                return;
            }
            var rect = svg.Picture!.CullRect;
            Width = (int)rect.Width;
            Height = (int)rect.Height;
        }

        public SvgImageSource(string content)
            : this(SKSvg.CreateFromSvg(content))
        {
        }

        public SvgImageSource(SvgImageData data)
            : this (data.Content)
        {
            
        }

        public SvgImageSource(SvgFileImageData data)
            : this(SKSvg.CreateFromFile(data.FileName))
        {

        }

        public SKSvg Source { get; set; }

        public override object? CreateThumbnail(Size size)
        {
            return Thumbnail.Snapshot(size, Source.Picture);
        }

        public override void Paint(IImageStyleCanvas canvas, IImageStyle computedStyle)
        {
            (canvas as ISKImageCanvas)?.Draw(Source.Picture, computedStyle);
        }
    }
}
