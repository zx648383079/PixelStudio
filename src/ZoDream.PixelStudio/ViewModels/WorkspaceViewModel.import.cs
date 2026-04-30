using Microsoft.UI.Xaml.Controls;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using ZoDream.PixelStudio.Plugins;
using ZoDream.Shared.Document;
using ZoDream.Shared.Drawing;
using ZoDream.Shared.ImageEditor;
using ZoDream.Shared.ImageEditor.Sources;
using ZoDream.Shared.Interfaces;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class WorkspaceViewModel
    {
        /// <summary>
        /// 分离图片
        /// </summary>
        /// <param name="layer"></param>
        private async void SeparateImage(IImageLayer layer, IEnumerable<SKPath> items)
        {
            var image = (BitmapImageSource)layer.Source;
            Add(items.Select(path => {
                var bound = path.Bounds;
                var kid = image.Source.Clip(path);
                if (kid is null)
                {
                    return null;
                }
                var kidLayer = new BitmapImageSource(
                    kid
                    , Instance!)
                {
                    X = (int)bound.Left,
                    Y = (int)bound.Top
                };
                return Create(kidLayer);
            }).Where(i => i is not null).Select(i => i!), layer);
            layer.IsVisible = false;
        }
        /// <summary>
        /// 分离图片对象并重新采样合并到子对象
        /// </summary>
        /// <param name="layer"></param>
        private async void SeparateImageAndMerge(IImageLayer layer, IEnumerable<SKPath> items)
        {
            var image = (BitmapImageSource)layer.Source;
            using var paint = new SKPaint();
            foreach (var item in layer.Children)
            {
                if (item.Source is not BitmapImageSource i)
                {
                    continue;
                }
                var path = GetContainPath(i, items);
                if (path is null)
                {
                    continue;
                }
                using var canvas = new SKCanvas(i.Source);
                var rect = path.Bounds;
                canvas.Clear(SKColors.Transparent);
                canvas.DrawBitmap(image.Source, rect, 
                    SKRect.Create(rect.Left - i.X, rect.Top - i.Y, rect.Width, rect.Height), paint);
                path.Offset(-i.X, -i.Y);
                canvas.ClipPath(path, SKClipOperation.Difference);
                canvas.Clear();
                item.Resample();
            }
        }

        private static SKPath? GetContainPath(IImageBound image, IEnumerable<SKPath> items)
        {
            var maxDiff = 6;
            var doubleMaxDiff = 2 * maxDiff;
            var minDiff = 2;
            var doubleMinDiff = 2 * minDiff;
            foreach (var item in items)
            {
                var bound = item.Bounds;
                if (image.X > bound.Left + minDiff || image.Y > bound.Top + minDiff
                    || image.Width < bound.Width - doubleMinDiff 
                    || image.Height < bound.Height - doubleMinDiff)
                {
                    continue;
                }
                if (bound.Left - image.X >= maxDiff 
                    || bound.Top - image.Y >= maxDiff
                    || image.Width - bound.Width >= doubleMaxDiff
                    || image.Height - bound.Height >= doubleMaxDiff)
                {
                    continue;
                }
                return item;
            }
            return null;
        }

        private async void TapImportFolder()
        {
            var picker = new FolderPicker();
            _app.InitializePicker(picker);
            var folder = await picker.PickSingleFolderAsync();
            if (folder != null) 
            {
                OnDragImage(new FileLoader(folder));
            }
        }

        private async void TapImportFile()
        {
            var picker = new FileOpenPicker();
            foreach (var ext in ReaderFactory.FileFilterItems)
            {
                picker.FileTypeFilter.Add(ext);
            }
            picker.FileTypeFilter.Add("*");
            _app.InitializePicker(picker);
            var items = await picker.PickMultipleFilesAsync();
            OnDragImage(new FileLoader(items));
        }

        protected override async void TapImport()
        {
            var picker = _app.CreatePickPlugin(PluginMenuItem.ImportName);
            if (await _app.OpenDialogAsync(picker) != ContentDialogResult.Primary)
            {
                return;
            }
            TapImportPlugin(picker.SelectedItem, picker.FileName);
        }

        private async Task<IImageLayer?> AddImageAsync(string? fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName) || !File.Exists(fileName))
            {
                return null;
            }
            var layer = AddImage(await ReaderFactory.LoadImageAsync(fileName));
            if (layer is not null)
            {
                layer.Name = Path.GetFileNameWithoutExtension(fileName);
            }
            if (layer is null)
            {
                return null;
            }
            _ = LoadImageMetaAsync(fileName, layer.Id);
            return layer;
        }

        public override void DragFiles(IEnumerable<IStorageItem> items)
        {
            OnDragImage(new FileLoader(items));
        }

        private IImageLayer? AddImage(IImageData data)
        {
            var layer = data.TryParse(Instance!);
            if (layer is null)
            {
                return null;
            }
            return Add(layer);
        }
        private async void OnDragImage(FileLoader loader)
        {
            IsLoading = true;
            if (!await loader.LoadAsync())
            {
                IsLoading = false;
                return;
            }
            await foreach (var item in loader.EnumerateImage())
            {
                var layer = AddImage(item.Source);
                if (layer is not null)
                {
                    layer.Name = Path.GetFileNameWithoutExtension(item.FileName);
                    AddLink(layer.Id, item.MetaItems);
                    AddLink(layer.Id, Path.GetFileName(item.FileName), layer.Name);
                }
            }
            await foreach (var item in loader.EnumerateLayer())
            {
                await ImportSpriteAsync(item);
            }
            await foreach (var item in loader.EnumerateSkeleton())
            {
                AddSkin(item.Skins);
                AddSlot(item.Slots);
                AddAnimation(item.Animations);
                if (item is ISkeletonController ctl)
                {
                    _styleManager.Add(new SkeletonImageStyler("ctl", ctl));
                } else if (item is SkeletonSection s)
                {
                    _styleManager.Add(new SkeletonImageStyler(s));
                }
            }
            AddResource(loader.ResourceItems);
            Instance!.Resize();
            Instance.Invalidate();
            IsLoading = false;
        }

        private async Task ImportSpriteAsync(ISpriteSection data)
        {
            if (data.Items.Count == 0)
            {
                return;
            }
            var parentLayer = GetLayerWithLink(data.Name!) ?? await AddImageAsync(data.FileName);
            if (parentLayer is null)
            {
                return;
            }
            if (parentLayer.Source is not BitmapImageSource layerImage)
            {
                return;
            }
            foreach (var kid in data.Items)
            {
                if (data is SpriteLayerSection s)
                {
                    kid.X = s.ComputeX(layerImage, kid);
                    kid.Y = s.ComputeY(layerImage, kid);
                }
                var kidLayer = layerImage.Split(kid);
                if (kidLayer is null)
                {
                    continue;
                }
                Add(Create(kidLayer, kid.Name), parentLayer);
            }
            parentLayer.IsVisible = false;
        }
        
    }
}
