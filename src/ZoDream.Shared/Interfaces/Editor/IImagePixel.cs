using SkiaSharp;
using System;
using System.IO;
using System.Numerics;

namespace ZoDream.Shared.Interfaces
{
    /// <summary>
    /// 获取图片数据
    /// </summary>
    public interface IImagePixel : IDisposable
    {
        public SKSize Size { get; }

        public SKImage Picture { get; }

        public void SaveAs(Stream output);
        public void SaveAs(string fileName);
    }
}
