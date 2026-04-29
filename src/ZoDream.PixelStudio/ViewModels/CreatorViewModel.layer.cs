using CommunityToolkit.Mvvm.ComponentModel;
using SkiaSharp;
using System;
using System.Collections.Generic;
using ZoDream.Shared.ImageEditor;
using ZoDream.Shared.Interfaces;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class CreatorViewModel : ILayerController
    {
        public IImageLayer? Current => throw new NotImplementedException();

        public void Add(IEnumerable<IImageLayer> items, IImageLayer? parent)
        {
            throw new NotImplementedException();
        }

        public void Add(IImageLayer layer, IImageLayer? parent)
        {
            throw new NotImplementedException();
        }

        public IImageLayer Add(IImageSource source)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IImageLayer> Get(SKRect rect)
        {
            throw new NotImplementedException();
        }

        public void InsertAfter(IEnumerable<IImageLayer> items, IImageLayer layer)
        {
            throw new NotImplementedException();
        }

        public void Paint(IImageCanvas canvas)
        {
            throw new NotImplementedException();
        }

        public void Remove(IImageLayer layer)
        {
            throw new NotImplementedException();
        }

        public bool TryGet(SKPoint point, out IImageLayer? layer)
        {
            throw new NotImplementedException();
        }
    }
}
