using ZoDream.Shared.Interfaces;
using ZoDream.Shared.UndoRedo;

namespace ZoDream.Shared.ImageEditor.Commands
{
    public class RotateCommand(IImageStyle source, float from, float to) : IBackableCommand
    {
        public RotateCommand(IImageStyle source, float to)
            : this(source, source.Rotate, to)
        {
            
        }

        public bool Execute()
        {
            source.Rotate = to;
            return true;
        }

        public void Undo()
        {
            source.Rotate = from;
        }
    }
}
