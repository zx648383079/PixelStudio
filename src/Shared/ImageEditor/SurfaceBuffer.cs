using SkiaSharp;
using System.IO;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor
{
    public class SurfaceBuffer(SKSurface surface) : IImagePixel
    {
        public Size Size => surface.Canvas.LocalClipBounds.Size.ToSize();

        public object Picture {
            get {
                using var image = surface.Snapshot();

                using var recorder = new SKPictureRecorder();

                var rect = new SKRect(0, 0, image.Width, image.Height);
                var recorderCanvas = recorder.BeginRecording(rect);

                recorderCanvas.DrawImage(image, 0, 0);

                var picture = recorder.EndRecording();

                return picture;
            }
        }

        public void Dispose()
        {
            surface.Dispose();
        }

        public void SaveAs(Stream output)
        {
            using var image = surface.Snapshot();
            image.Encode(output, SKEncodedImageFormat.Png, 100);
        }

        public void SaveAs(string fileName)
        {
            using var fs = File.Create(fileName);
            SaveAs(fs);
        }
    }
}
