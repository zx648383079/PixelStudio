using SkiaSharp;
using System;

namespace ZoDream.Shared.Interfaces
{
    public interface IImageEditor: IDisposable
    {
        public IImageOptions Options { get; }

        public ILayerController Layer { get; }
        public IImageController Controller { get; }

        public SKColor? BackgroundColor { get; set; }
        /// <summary>
        /// 当前计算的样式
        /// </summary>
        public IImageComputedStyler ComputedStyler { get; }

        public SKSize Size { get; }

        public IImageLayer? Current { get; }


        public void Initialize();

        /// <summary>
        /// 切换模式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void SwitchMode<T>() where T : ICommandController;


        public void Touch(SKPoint point);

        public IImageLayer AddFolder(string name);

        public IImageBuffer Create(string name);
        
        public void Select(IImageLayer? layer);

        public void Select(SKRect rect);

        public IImagePixel Encode(IImageLayer layer);
        public IImageBuffer Decode(IImageLayer layer);

        public void Unselect();
        public void Resize();
        public void Resize(SKSize size);

        public void Paint(SKCanvas canvas, SKImageInfo info);

        public void Invalidate();

        public void SaveAs(string fileName);

        public void SaveAs(IImageLayer layer, string fileName);
        
    }
}
