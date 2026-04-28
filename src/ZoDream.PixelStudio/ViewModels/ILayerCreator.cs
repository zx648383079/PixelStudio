using ZoDream.Shared.Interfaces;

namespace ZoDream.PixelStudio.ViewModels
{
    public interface ILayerCreator
    {
        public bool TryCreate(IImageEditor editor);
    }
}
