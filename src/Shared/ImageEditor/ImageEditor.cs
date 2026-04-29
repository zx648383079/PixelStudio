using SkiaSharp;
using System;
using System.Collections.Generic;
using ZoDream.Shared.ImageEditor.Layers;
using ZoDream.Shared.Interfaces;

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
        private readonly IList<ICommandLayer> _bottomLayers = [];
        private readonly IList<ICommandLayer> _topLayers = [];
        public ILayerController Layer { get; private set; }
        public IImageController Controller { get; private set; }
        public ICommandController? Commander { get; private set; }

        public IImageOptions Options { get; private set; } = new DefaultImageOptions();

        public SKSize Size { get; private set; } = SKSize.Empty;

        public IImageLayer? Current => Layer?.Current;

        public void Initialize()
        {
            _bottomLayers.Add(new TransparentLayer(this));
            _bottomLayers.Add(new GlyphLayoutLayer(this));
        }
        public void Resize()
        {

        }
        public void Resize(SKSize size)
        {
            Size = size;
            _shell.Resize(size);
            foreach (var layer in _bottomLayers)
            {
                layer.Invalidate();
            }
            foreach (var layer in _topLayers)
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

        public void Paint(SKCanvas canvas, SKImageInfo info)
        {
            canvas.Clear(SKColors.Transparent);
            var c = new ImageCanvas(canvas);
            foreach (var layer in _bottomLayers)
            {
                if (!layer.IsVisible)
                {
                    continue;
                }
                layer.Paint(c);
            }
            Layer?.Paint(c);
            Commander?.Paint(c);
            foreach (var layer in _topLayers)
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

        public void Select(SKRect rect)
        {
        }

        public void Touch(SKPoint point)
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
            foreach (var layer in _bottomLayers)
            {
                layer.Dispose();
            }
            foreach (var layer in _topLayers)
            {
                layer.Dispose();
            }
            Options?.Dispose();
        }
    }
}
