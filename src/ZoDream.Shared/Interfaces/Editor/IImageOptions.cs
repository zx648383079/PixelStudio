using System;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.Interfaces
{
    public interface IImageOptions : IDisposable
    {
        public IImagePaint JointStrokePaint { get; }
        public IImagePaint JointPaint { get; }
        public IImagePaint JointHoveredPaint { get; }
        public float JointSize { get; }

        public Color Hovered {  get; }
        public Color Activated {  get; }
        public Color Background {  get; }
        public IImagePaint BackgroundPaint {  get; }
        public Color Foreground {  get; }
        public IImagePaint ForegroundPaint {  get; }
        public IImagePaint TitlePaint {  get; }
        public IImagePaint TextPaint {  get; }


        public IImagePaint CreateBorder(Color color, float strokeWidth = 1);
        public IImagePaint CreateFill(Color color);
        public IFontPaint CreateFont(Color color, int fontSize);
        /// <summary>
        /// 生成缩略图创建器
        /// </summary>
        /// <returns></returns>
        public IThumbnailBuilder CreateThumbnail();
    }
}
