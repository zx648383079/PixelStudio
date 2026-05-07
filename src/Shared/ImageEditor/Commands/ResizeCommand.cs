using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;
using ZoDream.Shared.UndoRedo;

namespace ZoDream.Shared.ImageEditor.Commands
{
    public class ResizeCommand(IImageBound source, Rect from, Rect to) : IBackableCommand
    {
        public ResizeCommand(IImageBound source, Rect to)
            : this(source, new(source.X, source.Y, source.Width, source.Height), to)
        {
            
        }

        public ResizeCommand(IImageBound source, Size to)
            : this(source, new Rect(source.X, source.Y, to.Width, to.Height))
        {
            
        }

        public bool Execute()
        {
            source.X = to.X;
            source.Y = to.Y;
            source.Width = to.Width;
            source.Height = to.Height;
            return true;
        }

        public void Undo()
        {
            source.X = from.X;
            source.Y = from.Y;
            source.Width = from.Width;
            source.Height = from.Height;
        }
    }
}
