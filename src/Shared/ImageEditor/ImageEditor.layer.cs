using SkiaSharp;
using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor
{
    public partial class ImageEditor : IImageEditor
    {
        public SKColor? BackgroundColor { get; set; }

        public IImageComputedStyler ComputedStyler { get; private set; }

        public IImageLayer Add(IImageBuffer buffer, IImageLayer? parent)
        {
            throw new System.NotImplementedException();
        }

        public IImageLayer AddFolder(string name)
        {
            throw new System.NotImplementedException();
        }
        public IImageBuffer Create(string name)
        {
            throw new System.NotImplementedException();
        }

        public IImageBuffer Decode(IImageLayer layer)
        {
            throw new System.NotImplementedException();
        }

        public IImagePixel Encode(IImageLayer layer)
        {
            throw new System.NotImplementedException();
        }

        public void SaveAs(string fileName)
        {
            throw new System.NotImplementedException();
        }

        public void SaveAs(IImageLayer layer, string fileName)
        {
            throw new System.NotImplementedException();
        }

    }
}
