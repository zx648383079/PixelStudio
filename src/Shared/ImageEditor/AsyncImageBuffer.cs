using SkiaSharp;
using System.IO;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor
{
    public class AsyncImageBuffer(IImageData data) : IImagePixel, ISKImagePixel
    {
        private SKImage? _image;

        public SKImage Source {
            get {
                _image ??= data.ToImage();
                return _image;
            }
        }
        public Size Size => new(Source.Width, Source.Height);

        public object Picture {
            get {
                var recorder = new SKPictureRecorder();
                var canvas = recorder.BeginRecording(SKRect.Create(0, 0, Source.Width, Source.Height));
                canvas.DrawImage(Source, 0, 0, SKSamplingOptions.Default);
                var picture = recorder.EndRecording();
                recorder.Dispose();
                return picture;
            }
        }

   

        public void SaveAs(Stream output)
        {
            Source.Encode(output, SKEncodedImageFormat.Png, 100);
        }

        public void SaveAs(string fileName)
        {
            using var fs = File.Create(fileName);
            SaveAs(fs);
        }

        public void Paint(SKCanvas canvas, SKPoint point, SKPaint? paint = null)
        {
            canvas.DrawImage(Source, point, SKSamplingOptions.Default, paint);
        }

        public void Paint(SKCanvas canvas, SKRect rect, SKPaint? paint = null)
        {
            canvas.DrawImage(Source, rect, SKSamplingOptions.Default, paint);
        }

        public void Paint(SKCanvas canvas, SKPoint[] sourceVertices, SKPoint[] vertices)
        {
            using var paint = new SKPaint()
            {
                IsAntialias = true,
                Shader = SKShader.CreateImage(Source, SKShaderTileMode.Clamp, SKShaderTileMode.Clamp)
            };
            canvas.DrawVertices(SKVertexMode.TriangleFan,
                vertices,
                sourceVertices, null, paint);
        }

        public void Dispose()
        {
            _image?.Dispose();
        }
    }
}