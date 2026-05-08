using SkiaSharp;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.ImageEditor.Sources;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor
{
    public partial class ImageEditor : IImageEditor
    {
        public Color? BackgroundColor { get; set; }

        public IImageComputedStyler ComputedStyler { get; private set; }



        public IImageLayer Add(IImageBuffer buffer, IImageLayer? parent)
        {
            throw new System.NotImplementedException();
        }

        public IImageLayer AddFolder(string name)
        {
            return Layer.Add(new FolderImageSource(), name);
        }
        public IImageBuffer Create(string name)
        {
            return new ImageBuffer(Size);
        }

        public IImageBuffer Decode(IImageLayer layer)
        {
            return new ImageBuffer(layer.Source.Bound.ToSize());
        }

        public IImagePixel Encode(IImageLayer layer)
        {
            return new ImagePixel(layer);
        }

        public void SaveAs(string fileName)
        {
            var styler = Compute();
            using var bitmap = new SKBitmap((int)styler.ActualWidth, (int)styler.ActualHeight);
            using var canvas = new SKCanvas(bitmap);
            var c = new ImageStyleCanvas(canvas, styler);
            styler.Paint(Layer.Items, c);
            bitmap.SaveAs(fileName);
        }

        public void SaveAs(IImageLayer layer, string fileName)
        {
            if (layer is null)
            {
                return;
            }
            var styler = new ImageComputedStyler(Controller.RealStyler);
            styler.Compute(layer);
            var bitmap = new SKBitmap((int)styler.ActualWidth, (int)styler.ActualHeight);
            using (var surface = new SKCanvas(bitmap))
            {
                var c = new ImageStyleCanvas(surface, styler);
                layer.Paint(c);
            }
            bitmap.SaveAs(fileName);
        }

        /// <summary>
        /// 先计算
        /// </summary>
        /// <returns></returns>
        private IImageComputedStyler Compute()
        {
            var source = Controller.Styler;
            var styler = source is IImageComputedStyler s ?
               s : new ImageComputedStyler(source);
            styler.Clear();
            styler.Compute(Layer.Items);
            if ((styler.ActualWidth == 0
                || styler.ActualHeight == 0)
                && styler is IImageSize c)
            {
                c.Width = Size.Width;
                c.Height = Size.Height;
            }
            return styler;
        }

    }
}
