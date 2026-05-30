using SkiaSharp;
using System;
using System.Collections.Generic;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor
{
    public class ImageBuilder : IImageBuilder
    {

        public ImageBuilder()
        {
            
        }

        public ImageBuilder(Size size)
        {
            _canvasSize = size;
        }

        public ImageBuilder(IImageEditor editor)
        {
            _leaveOpen = true;
            _canvasSize = editor.Size;
            Options = editor.Options;
        }

        private readonly bool _leaveOpen = false;
        private readonly Size _canvasSize;
        private SKBitmap? _image;

        public IImageOptions Options { get; private set; } = new DefaultImageOptions();

        public Color? BackgroundColor { get; set; }

        public Thickness Padding { get; set; }

        public IList<IImageSource> Items { get; private set; } = [];

        public Size Size {
            get {
                if (!_canvasSize.IsEmpty)
                {
                    return _canvasSize;
                }
                var outerWidth = 1f;
                var outerHeight = 1f;
                foreach (var item in Items)
                {
                    var bound = item.Bound;
                    outerWidth = Math.Max(outerWidth, bound.Right);
                    outerHeight = Math.Max(outerHeight, bound.Bottom);
                }
                return new(outerWidth, outerHeight);
            }
        }

        public void Add(IImageSource source)
        {
            Items.Add(source);
        }

        public SKBitmap Build()
        {
            if (_image is not null)
            {
                return _image;
            }
            var size = Size.ToSizeI();
            if (_image is null || size.Width != _image.Width || size.Height != _image.Height)
            {
                _image?.Dispose();
                var info = new SKImageInfo(size.Width, size.Height);
                _image = new SKBitmap(info);
            }
            var canvas = new ImageCanvas(new SKCanvas(_image));
            canvas.Clear();
            foreach (var item in Items)
            {
                item.Paint(canvas);
            }
            return _image;
        }

        public IImagePixel Encode()
        {
            return ImagePixel.From(Build());
        }

        public void SaveAs(string fileName)
        {
            Build().SaveAs(fileName);
        }

        public void Dispose()
        {
            foreach (var item in Items)
            {
                item.Dispose();
            }
            _image?.Dispose();
            if (_leaveOpen)
            {
                return;
            }
            Options.Dispose();
        }
    }
}
