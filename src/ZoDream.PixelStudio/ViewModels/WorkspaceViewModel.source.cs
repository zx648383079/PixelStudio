using Microsoft.UI.Xaml.Media.Imaging;
using SkiaSharp;
using SkiaSharp.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using ZoDream.Shared.ImageEditor;
using ZoDream.Shared.Interfaces;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class WorkspaceViewModel : IImageController, ILayerController
    {

        private readonly ImageStyleManager _styleManager = [];


        public IImageStyler Styler => _styleManager.TryGet(_styleMode, out var s) ? s : DefaultStyler;

        public IImageStyler DefaultStyler => _styleManager.Default;

        public IImageStyler RealStyler => _styleManager.Real;

        public IImageLayerTree Source => LayerItems;


        public bool LayerMode {
            get => !string.IsNullOrWhiteSpace(_styleMode) && 
                _styleMode != DefaultStyler.Name;
            set {
                OnPropertyChanged(nameof(LayerMode));
                _styleMode = value ? _styleManager.Last().Name : DefaultStyler.Name;
                OnPropertyChanged(nameof(StyleMode));
                Instance?.Invalidate();
            }
        }

        private string _styleMode;

        public string StyleMode {
            get => _styleMode;
            set {
                SetProperty(ref _styleMode, value);
                Instance?.Invalidate();
            }
        }


        private LayerTree _layerItems = [];

        public LayerTree LayerItems {
            get => _layerItems;
            set => SetProperty(ref _layerItems, value);
        }

        public IImageLayer? Current => throw new NotImplementedException();

        public IImageLayer? GetLayer(Guid id)
        {
            return LayerItems.Get(id);
        }

        public IImageLayer? GetLayer(string name)
        {
            return LayerItems.Get(item => item.Name == name);
        }


        public IImageLayer Create(IImageSource source)
        {
            return new LayerViewModel(this, source)
            {
                PreviewImage = CreateThumbnail(source)
            };
        }

        public IImageLayer Create(IImageSource source, string name)
        {
            return new LayerViewModel(this, source)
            {
                Name = name,
                PreviewImage = CreateThumbnail(source)
            };
        }

        public void Add(IEnumerable<IImageLayer> items, IImageLayer? parent = null)
        {
            foreach (var item in items.Reverse())
            {
                if (item is null)
                {
                    continue;
                }
                Add(item, parent);
            }
        }

        private void Initialize(IImageLayer layer)
        {
            if (string.IsNullOrWhiteSpace(layer.Name))
            {
                layer.Name = $"undefined_{layer.Id}";
            }
        }

        public void Add(IImageLayer layer, IImageLayer? parent = null)
        {
            if (parent is null)
            {
                if (LayerItems.Contains(layer))
                {
                    return;
                }
                layer.Parent?.Children.Remove(layer);
                Initialize(layer);
                layer.Depth = 0;
                layer.Parent = null;
                LayerItems.AddFirst(layer);
                return;
            }
            Initialize(layer);
            layer.Depth = parent.Depth + 1;
            if (parent != layer.Parent)
            {
                if (layer.Parent is null)
                {
                    LayerItems.Remove(layer);
                }
                else
                {
                    layer.Parent.Children.Remove(layer);
                }
                layer.Parent = parent;
            }
            parent.Children.AddFirst(layer);
        }

        public IImageLayer Add(IImageSource source)
        {
            var layer = Create(source);
            Add(layer);
            return layer;
        }

        public void InsertAfter(IEnumerable<IImageLayer> items, IImageLayer layer)
        {
            var parent = layer.Parent;
            var tree = parent is null ? LayerItems : parent.Children;
            var i = tree.IndexOf(layer) + 1;
            foreach (var item in items.Reverse())
            {
                item.Parent = parent;
                item.Depth = layer.Depth;
                tree.Insert(i, item);
            }
        }

        public void Remove(IImageLayer layer)
        {
            if (layer.Parent is null)
            {
                LayerItems.Remove(layer);
            }
            else
            {
                layer.Parent.Children.Remove(layer);
            }
            layer.Dispose();
        }

        public void Clear()
        {
            foreach (var item in LayerItems)
            {
                item.Dispose();
            }
            LayerItems.Clear();
        }

        public bool TryGet(SKPoint point, out IImageLayer? layer)
        {
            layer = LayerItems.Get(point);
            return layer != null;
        }

        public IEnumerable<IImageLayer> Get(SKRect rect)
        {
            throw new NotImplementedException();
        }

        public void Paint(IImageCanvas canvas)
        {
            LayerItems.Paint(canvas);
        }
    }
}
