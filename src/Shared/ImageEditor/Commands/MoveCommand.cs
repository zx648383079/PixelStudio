using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;
using ZoDream.Shared.UndoRedo;

namespace ZoDream.Shared.ImageEditor.Commands
{
    public class MoveCommand(IImagePoint source, Point from, Point to) : IBackableCommand
    {
        public MoveCommand(IImagePoint source, Point to)
            : this(source, new(source.X, source.Y), to)
        {
            
        }
        public bool Execute()
        {
            source.X = to.X;
            source.Y = to.Y;
            return true;
        }

        public void Undo()
        {
            source.X = from.X;
            source.Y = from.Y;
        }
    }
}
