using ZoDream.Shared.Interfaces;
using ZoDream.Shared.UndoRedo;

namespace ZoDream.Shared.ImageEditor.Commands
{
    public class RemoveLayerCommand(ILayerController controller, IImageLayer layer) : IBackableCommand
    {
        public bool Execute()
        {
            controller.Remove(layer);
            return true;
        }

        public void Undo()
        {
            controller.Add(layer);
        }
    }
}
