using System;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.Interfaces
{
    public interface IImageSource: IDisposable
    {
        public Rect Bound { get; }

        public bool Contains(Point point);

        /// <summary>
        /// 生成预览图
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public object? CreateThumbnail(Size size);


        public void Paint(IImageCanvas canvas);
    }

    public interface IImageStyleSource : IImageSource, IImageStyle
    {

        public void Paint(IImageStyleCanvas canvas, IImageStyle computedStyle);
    }
}
