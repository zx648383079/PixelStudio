using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.ImageEditor.Layers;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor
{
    public partial class ImageEditor
    {

        public ImageEditor(IImageShell shell, 
            IImageController controller,
            ILayerController layer,
            IImageService service)
        {
            shell.Bus = this;
            _shell = shell;
            Controller = controller;
            Layer = layer;
            Service = service;
        }

        private readonly IImageShell _shell;

        public IImageService Service { get; private set; }
        public IList<ICommandLayer> BackBar { get; private set; } = [];
        public IList<ICommandLayer> FrontBar { get; private set; } = [];
        public ILayerController Layer { get; private set; }
        public IImageController Controller { get; private set; }
        public ICommandController? Commander { get; private set; }

        public IImageOptions Options { get; private set; } = new DefaultImageOptions();

        public Size Size { get; private set; } = new();

        public Thickness Padding { get; set; } = new();

        public void Initialize()
        {
            BackBar.Add(new TransparentLayer(this));
        }

        public void Resize()
        {
            var outerWidth = 0f;
            var outerHeight = 0f;
            foreach (var item in Layer.Items)
            {
                var bound = item.Source.Bound;
                outerWidth = Math.Max(outerWidth, bound.Right);
                outerHeight = Math.Max(outerHeight, bound.Bottom);
            }
            if (outerHeight == 0 || outerWidth == 0)
            {
                return;
            }
            Resize(new(outerWidth, outerHeight));
        }
        public void Resize(Size size)
        {
            Size = size;
            _shell.Resize(size);
            foreach (var layer in BackBar)
            {
                layer.Invalidate();
            }
            foreach (var layer in FrontBar)
            {
                layer.Invalidate();
            }
        }

        public void SwitchMode<T>() where T : ICommandController
        {
            if (Commander is T)
            {
                return;
            }
            Commander?.Dispose();
            Commander = (T)Activator.CreateInstance(typeof(T), this);
            Commander?.Initialize(Layer.SelectedItems);
            Invalidate();
        }

        public void Invalidate()
        {
            _shell.Invalidate();
        }
        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="delta">上次的间隔/s</param>
        public void Paint(ICanvasShell canvas, float delta)
        {
            canvas.Clear(SKColors.Transparent.ToColor());
            var c = canvas.ToCanvas(Compute());
            foreach (var layer in BackBar)
            {
                if (!layer.IsVisible)
                {
                    continue;
                }
                layer.Paint(c);
            }
            Layer?.Paint(c);
            Commander?.Paint(c);
            foreach (var layer in FrontBar)
            {
                if (!layer.IsVisible)
                {
                    continue;
                }
                layer.Paint(c);
            }
        }

        public void Select(IImageLayer? layer)
        {
            Commander?.Initialize(layer is null ? [] : [layer]);
        }

        public void Select(Rect rect)
        {
            var items = Layer.Get(rect).ToArray();
            Commander?.Initialize(items);
        }

        public void Touch(Point point)
        {
            if (Layer.TryGet(point, out var layer))
            {
                Select(layer);
            }
        }

        public void Unselect()
        {
            Commander?.Initialize([]);
            Invalidate();
        }

        public void Add(IImageSource source)
        {
            Layer?.Add(source);
        }

        public void Dispose()
        {
            Commander?.Dispose();
            foreach (var layer in BackBar)
            {
                layer.Dispose();
            }
            foreach (var layer in FrontBar)
            {
                layer.Dispose();
            }
            Options?.Dispose();
        }
    }
}
