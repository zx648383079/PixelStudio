using SkiaSharp;
using System.IO;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor
{
    public class ImagePixel(IImageLayer layer) : IImagePixel
    {
        public Size Size => layer.Source.Bound.ToSize();

        public object Picture {
            get {
                var recorder = new SKPictureRecorder();
                var size = Size;
                var canvas = recorder.BeginRecording(SKRect.Create(0, 0, size.Width, size.Height));
                layer.Paint(new ImageCanvas(canvas));
                var picture = recorder.EndRecording();
                recorder.Dispose();
                return picture;
            }
        }



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

        public static IImagePixel From(SKBitmap bitmap)
        {
            return new BitmapBuffer(bitmap);
        }

        public static IImagePixel From(SKSurface surface)
        {
            return new SurfaceBuffer(surface);
        }

        public static IImagePixel From(IImageData data)
        {
            return new AsyncImageBuffer(data);
        }
    }
}
