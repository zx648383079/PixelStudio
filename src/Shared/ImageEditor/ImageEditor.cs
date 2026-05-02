using SkiaSharp;
using System;
using System.Collections.Generic;
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
            ILayerController layer)
        {
            shell.Bus = this;
            _shell = shell;
            Controller = controller;
            Layer = layer;
        }

        private readonly IImageShell _shell;
        public IList<ICommandLayer> BackBar { get; } = [];
        public IList<ICommandLayer> FrontBar { get; } = [];
        public ILayerController Layer { get; private set; }
        public IImageController Controller { get; private set; }
        public ICommandController? Commander { get; private set; }

        public IImageOptions Options { get; private set; } = new DefaultImageOptions();

        public Size Size { get; private set; } = new();

        public Thickness Padding { get; set; } = new();

        public IImageLayer? Current => Layer?.Current;

        public void Initialize()
        {
            BackBar.Add(new TransparentLayer(this));
        }

        public void Resize()
        {

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
            Commander?.Initialize(Current);
            Invalidate();
        }

        public void Invalidate()
        {
            _shell.Invalidate();
        }

        public void Paint(ICanvasShell canvas)
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
            Commander?.Initialize(layer);
        }

        public void Select(Rect rect)
        {
        }

        public void Touch(Point point)
        {

        }

        public void Unselect()
        {
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
