using SkiaSharp;
using Svg.Skia;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor.Sources
{
    public class SvgImageSource : BaseImageSource
    {

        public SvgImageSource(SKSvg svg, IImageEditor editor)
            : base(editor)
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

        public SvgImageSource(string content, IImageEditor editor)
            : this(SKSvg.CreateFromSvg(content), editor)
        {
        }

        public SvgImageSource(SvgImageData data, IImageEditor editor)
            : this (data.Content, editor)
        {
            
        }

        public SvgImageSource(SvgFileImageData data, IImageEditor editor)
            : this(SKSvg.CreateFromFile(data.FileName), editor)
        {

        }

        public SKSvg Source { get; set; }

        public override SKBitmap? CreateThumbnail(SKSize size)
        {
            return Thumbnail.Snapshot(size, Source.Picture);
        }

        public override void Paint(IImageCanvas canvas, IImageStyle computedStyle)
        {
            canvas.DrawPicture(Source.Picture, computedStyle);
        }
    }
}
