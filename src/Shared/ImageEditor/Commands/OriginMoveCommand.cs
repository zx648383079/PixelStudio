using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;
using ZoDream.Shared.UndoRedo;

namespace ZoDream.Shared.ImageEditor.Commands
{
    public class OriginMoveCommand(IImageStyleSource source, Point from, Point to) : IBackableCommand
    {
        public OriginMoveCommand(IImageStyleSource source, Point to)
            : this(source, source.Origin, to)
        {
            
        }

        public bool Execute()
        {
            source.Origin = to;
            return true;
        }

        public void Undo()
        {
            source.Origin = from;
        }
    }
}
