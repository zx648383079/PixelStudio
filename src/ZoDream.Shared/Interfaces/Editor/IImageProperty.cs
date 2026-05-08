using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.Interfaces
{
    public interface IImageProperty
    {
        public IImageOptions Options { get; }

        public Color? BackgroundColor { get; set; }

        public Size Size { get; }
        /// <summary>
        /// 内边距
        /// </summary>
        public Thickness Padding { get; set; }
    }
}
