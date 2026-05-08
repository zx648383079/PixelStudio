using ZoDream.Shared.Interfaces;
using ZoDream.Shared.UndoRedo;

namespace ZoDream.Shared.ImageEditor.Commands
{
    public class AddLayerCommand(ILayerController controller, IImageLayer layer) : IBackableCommand
    {
        public bool Execute()
        {
            controller.Add(layer);
            return true;
        }

        public void Undo()
        {
            controller.Remove(layer);
        }
    }
}
