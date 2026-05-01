using System;
using System.IO;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.Interfaces
{
    /// <summary>
    /// 获取图片数据
    /// </summary>
    public interface IImagePixel : IDisposable
    {
        public Size Size { get; }

        /// <summary>
        /// 实际转换为 SKPicture
        /// </summary>
        public object Picture { get; }

        public void SaveAs(Stream output);
        public void SaveAs(string fileName);
    }
}
