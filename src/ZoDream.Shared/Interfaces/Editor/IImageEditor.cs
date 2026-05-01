using System;
using System.Collections.Generic;
using ZoDream.Shared.Interfaces.Editor;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.Interfaces
{
    public interface IImageEditor: IDisposable
    {
        public IImageOptions Options { get; }
        /// <summary>
        /// 背景工具层，显示在最底层
        /// </summary>
        public IList<ICommandLayer> BackBar { get; }
        /// <summary>
        /// 前景工具层，显示在最上层
        /// </summary>
        public IList<ICommandLayer> FrontBar { get; }

        public ILayerController Layer { get; }
        public IImageController Controller { get; }

        public Color? BackgroundColor { get; set; }
        /// <summary>
        /// 当前计算的样式
        /// </summary>
        public IImageComputedStyler ComputedStyler { get; }

        public Size Size { get; }
        /// <summary>
        /// 内边距
        /// </summary>
        public Rect Padding { get; set; }

        public IImageLayer? Current { get; }


        public void Initialize();

        /// <summary>
        /// 切换模式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void SwitchMode<T>() where T : ICommandController;


        public void Touch(Point point);

        public IImageLayer AddFolder(string name);

        public IImageBuffer Create(string name);
        
        public void Select(IImageLayer? layer);

        public void Select(Rect rect);

        public IImagePixel Encode(IImageLayer layer);
        public IImageBuffer Decode(IImageLayer layer);

        public void Unselect();
        public void Resize();
        public void Resize(Size size);

        public void Paint(ICanvasShell canvas);

        public void Invalidate();

        public void SaveAs(string fileName);

        public void SaveAs(IImageLayer layer, string fileName);
        
    }
}
