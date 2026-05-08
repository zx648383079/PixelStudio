using System;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.Interfaces
{
    /// <summary>
    /// 使用代码绘制图片
    /// </summary>
    public interface IImageBuilder : IImageProperty, IDisposable
    {
        public void Add(IImageSource source);

        public IImagePixel Encode();

        public void SaveAs(string fileName);
    }

    public interface IThumbnailBuilder : IDisposable
    {
        public object Mutate(Size size, Action<IImageCanvas> action);
    }
}
