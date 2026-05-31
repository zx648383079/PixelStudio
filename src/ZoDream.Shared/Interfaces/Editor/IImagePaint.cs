using System;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.Interfaces
{
    /// <summary>
    /// 画笔
    /// </summary>
    public interface IImagePaint : IDisposable
    {
        public float StrokeWidth { get; set; }

        public bool IsStroke { get; set; }
        public Color Color { get; set; }
    }

    public interface IFontPaint : IImagePaint
    {
        /// <summary>
        /// 根据字符串计算大至区域
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Rect MeasureText(string text);
        /// <summary>
        /// 根据长度截断字符串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxWidth"></param>
        /// <returns></returns>
        public int BreakText(string text, float maxWidth);
    }
}
