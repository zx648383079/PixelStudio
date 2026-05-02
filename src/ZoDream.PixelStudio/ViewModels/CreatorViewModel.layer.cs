using System;
using System.Collections.Generic;
using System.Linq;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class CreatorViewModel : ILayerController
    {


        private IImageLayer? _selectedLayer;

        public IImageLayer? SelectedLayer {
            get => _selectedLayer;
            set {
                SetProperty(ref _selectedLayer, value);
                OnPropertyChanged(nameof(IsSelectedLayer));
            }
        }


        private LayerTree _layerItems = [];

        public LayerTree LayerItems {
            get => _layerItems;
            set => SetProperty(ref _layerItems, value);
        }

        public IImageLayer? Current => SelectedLayer;

        public IImageLayerTree Items => LayerItems;

        public void Add(IEnumerable<IImageLayer> items, IImageLayer? parent = null)
        {
            foreach (var item in items.Reverse())
            {
                Add(item);
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
            if (layer is LayerViewModel v && v.PreviewImage is null)
            {
                v.PreviewImage = CreateThumbnail(v.Source);
            }
            Initialize(layer);
            LayerItems.Add(layer);
        }

        public IImageLayer Add(IImageSource source)
        {
            var layer = new LayerViewModel(this, source)
            {
                PreviewImage = CreateThumbnail(source)
            };
            Add(layer, null);
            return layer;
        }

        public IImageLayer Add(IImageSource source, string name)
        {
            var layer = new LayerViewModel(this, source)
            {
                PreviewImage = CreateThumbnail(source),
                Name = name
            };
            Add(layer, null);
            return layer;
        }


        public void Clear()
        {
            foreach (var item in LayerItems)
            {
                item.Dispose();
            }
            LayerItems.Clear();
        }

        public IEnumerable<IImageLayer> Get(Rect rect)
        {
            foreach (var item in LayerItems)
            {
                if (item.Source.Bound.IsIntersect(rect))
                {
                    yield return item;
                }
            }
        }

        public void InsertAfter(IEnumerable<IImageLayer> items, IImageLayer layer)
        {
            var parent = layer.Parent;
            var i = LayerItems.IndexOf(layer) + 1;
            foreach (var item in items.Reverse())
            {
                item.Parent = parent;
                item.Depth = layer.Depth;
                LayerItems.Insert(i, item);
            }
        }

        public void Paint(IImageCanvas canvas)
        {
            foreach (var item in LayerItems.Reverse())
            {
                item.Paint(canvas);
            }
        }

        public void Remove(IImageLayer layer)
        {
            LayerItems.Remove(layer);
            layer.Dispose();
        }

        public bool TryGet(Point point, out IImageLayer? layer)
        {
            foreach (var item in LayerItems)
            {
                if (item.Source.Contains(point))
                {
                    layer = item;
                    return true;
                }
            }
            layer = null;
            return false;
        }
    }
}
