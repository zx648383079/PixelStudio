using System;
using System.IO;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor
{
    public class ImagePixel(IImageLayer layer) : IImagePixel
    {
        public Size Size => layer.Source.Bound.ToSize();

        public object Picture => throw new NotImplementedException();



        public void SaveAs(Stream output)
        {
        }

        public void SaveAs(string fileName)
        {
            using var fs = File.Create(fileName);
            SaveAs(fs);
        }

        public void Dispose()
        {
        }
    }
}
