using SkiaSharp;
using System;

namespace ZoDream.Shared.Interfaces
{
    public interface IImageSource: IDisposable
    {
        public SKRect Bound { get; }

        public bool Contains(SKPoint point);

        /// <summary>
        /// 生成预览图
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public SKBitmap? CreateThumbnail(SKSize size);


        public void Paint(IImageCanvas canvas);
    }

    public interface IImageStyleSource : IImageSource, IImageStyle
    {

        public void Paint(IImageCanvas canvas, IImageStyle computedStyle);
    }
}
