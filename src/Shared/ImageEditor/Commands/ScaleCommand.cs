using System.Numerics;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.UndoRedo;

namespace ZoDream.Shared.ImageEditor.Commands
{
    public class ScaleCommand(IImageStyle source, Vector2 from, Vector2 to) : IBackableCommand
    {
        public ScaleCommand(IImageStyle source, Vector2 to)
            : this(source, new(source.ScaleX, source.ScaleY), to)
        {
            
        }

        public bool Execute()
        {
            source.ScaleX = to.X;
            source.ScaleY = to.Y;
            return true;
        }

        public void Undo()
        {
            source.ScaleX = from.X;
            source.ScaleY = from.Y;
        }
        /// <summary>
        /// 左右翻转
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ScaleCommand CreateHorizontalFlip(IImageStyle source)
        {
            return new(source, new(-1, 1));
        }

        /// <summary>
        /// 上下翻转
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ScaleCommand CreateVerticalFlip(IImageStyle source)
        {
            return new(source, new(1, -1));
        }
    }
}
