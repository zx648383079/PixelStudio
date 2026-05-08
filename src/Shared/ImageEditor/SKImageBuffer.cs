using SkiaSharp;
using System.IO;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor
{
    public class SKImageBuffer(SKImage image) : IImagePixel
    {
        public Size Size => new(image.Width, image.Height);

        public SKImage Source => image;

        public object Picture {
            get {
                var recorder = new SKPictureRecorder();
                var canvas = recorder.BeginRecording(SKRect.Create(0, 0, image.Width, image.Height));
                canvas.DrawImage(image, 0, 0);
                var picture = recorder.EndRecording();
                recorder.Dispose();
                return picture;
            }
        }

        public void Dispose()
        {
            image.Dispose();
        }

        public void SaveAs(Stream output)
        {
            image.Encode(output, SKEncodedImageFormat.Png, 100);
        }

        public void SaveAs(string fileName)
        {
            using var fs = File.Create(fileName);
            SaveAs(fs);
        }
    }
}
