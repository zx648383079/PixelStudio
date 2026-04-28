using SkiaSharp;
using System;
using System.IO;
using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor
{
    public class ImagePixel(IImageLayer layer) : IImagePixel
    {
        public SKSize Size => layer.Source.Bound.Size;

        public SKImage Picture => throw new NotImplementedException();

        

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
