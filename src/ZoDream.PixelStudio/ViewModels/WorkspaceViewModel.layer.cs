using SkiaSharp;
using ZoDream.Shared.Extensions;
using ZoDream.Shared.ImageEditor;
using ZoDream.Shared.ImageEditor.Sources;
using ZoDream.Shared.Interfaces;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class WorkspaceViewModel
    {
        private void TapUngroup(IImageLayer? arg)
        {
            var layer = arg is not null ? arg : SelectedItem;
            if (layer is null)
            {
                return;
            }
            Instance?.Layer.InsertAfter(layer.Children, layer);
            layer.Children.Clear();
            Instance?.Layer.Remove(layer);
        }


        private async void TapLayerApply(IImageLayer? arg)
        {
            var layer = arg is not null ? arg : SelectedItem;
            if (layer is null)
            {
                return;
            }
            if (layer.Source is IImageStyle s && s.ScaleY == 1 && s.ScaleX == 1 && s.Rotate % 360 == 0)
            {
                return;
            }
            if (!await _app.ConfirmAsync("确定应用属性？将删除子图层..."))
            {
                return;
            }
            var styler = new ImageComputedStyler(RealStyler);
            styler.Compute(layer);
            var bitmap = new SKBitmap((int)styler.ActualWidth, (int)styler.ActualHeight);
            using var surface = new SKCanvas(bitmap);
            var c = new ImageStyleCanvas(surface, styler);
            layer.Paint(c);
            layer.Dispose();
            layer.Children.Clear();
            layer.Source = new BitmapImageSource(bitmap);
            layer.Resample();
            Instance?.Invalidate();
        }

        private void TapLayerRotate(object? arg)
        {
            var deg = 0;
            if (arg is int i)
            {
                deg = i;
            } else if (arg is string s)
            {
                _ = int.TryParse(s, out deg);
            }
            if (deg == 0 || SelectedItem is null)
            {
                return;
            }
            if (SelectedItem.Source is IImageStyle st)
            {
                st.Rotate += deg;
            }
            
            Instance?.Invalidate();
        }

        private void TapLayerScale()
        {

        }

        private void TapLayerScaleX()
        {

        }

        private void TapLayerScaleY()
        {

        }

        protected override void TapDeleteLayer(object? arg)
        {
            var layer = arg is IImageLayer o ? o : SelectedItem;
            if (layer is null)
            {
                return;
            }
            Instance?.Layer.Remove(layer);
            if (SelectedItem == layer)
            {
                SelectedItem = null;
                Instance?.Unselect();
            }
            Instance?.Invalidate();
        }

        protected override void TapLayerVisible(object? arg)
        {
            var layer = arg is IImageLayer o ? o : SelectedItem;
            if (layer is null)
            {
                return;
            }
            layer.IsVisible = true;
            Instance?.Invalidate();
        }

        protected override void TapLayerHidden(object? arg)
        {
            var layer = arg is IImageLayer o ? o : SelectedItem;
            if (layer is null)
            {
                return;
            }
            layer.IsVisible = false;
            Instance?.Invalidate();
        }

        protected override void TapAllVisible()
        {
            LayerItems.Get(item => {
                item.IsVisible = true;
                return false;
            });
            Instance?.Invalidate();
        }

        protected override void TapOtherHidden(object? arg)
        {
            var layer = arg is IImageLayer o ? o : SelectedItem;
            if (layer is null)
            {
                return;
            }
            LayerItems.Get(item => {
                if (item.Children.Count > 0)
                {
                    return false;
                }
                item.IsVisible = item == layer;
                return false;
            });
            Instance?.Invalidate();
        }

        protected override void TapOtherVisible(object? arg)
        {
            var layer = arg is IImageLayer o ? o : SelectedItem;
            if (layer is null)
            {
                return;
            }
            LayerItems.Get(item => {
                if (item.Children.Count > 0)
                {
                    return false;
                }
                item.IsVisible = item != layer;
                return false;
            });
            Instance?.Invalidate();
        }

        protected override void TapLayerVisibleToggle(object? arg)
        {
            var layer = arg is IImageLayer o ? o : SelectedItem;
            if (layer is null)
            {
                return;
            }
            layer.IsVisible = !layer.IsVisible;
            Instance?.Invalidate();
        }

        protected override void TapLayerLockToggle(object? arg)
        {
            var layer = arg is IImageLayer o ? o : SelectedItem;
            if (layer is null)
            {
                return;
            }
            layer.IsLocked = !layer.IsLocked;
        }

        protected override void TapLayerLock(object? arg)
        {
            var layer = arg is IImageLayer o ? o : SelectedItem;
            if (layer is null)
            {
                return;
            }
            layer.IsLocked = true;
        }

        protected override void TapLayerUnlock(object? arg)
        {
            var layer = arg is IImageLayer o ? o : SelectedItem;
            if (layer is null)
            {
                return;
            }
            layer.IsLocked = false;
        }

        protected override void TapAllUnlock()
        {
            LayerItems.Get(item => {
                item.IsLocked = false;
                return false;
            });
        }

        

        private void TapLayerHorizontalLeft(object? arg)
        {
            var layer = arg is IImageLayer o ? o : SelectedItem;
            if (layer is null)
            {
                return;
            }
            if (layer.Source is IImageStyle s)
            {
                s.X = 0;
            }
        }

        private void TapLayerHorizontalCenter(object? arg)
        {
            var layer = arg is IImageLayer o ? o : SelectedItem;
            if (layer is null)
            {
                return;
            }
            var (width, _) = GetParentSize(layer);
            if (layer.Source is IImageStyle s)
            {
                s.X = (width - s.Width) / 2;
            }
            
        }

        private (float, float) GetParentSize(IImageLayer layer)
        {
            var x = 0f;
            var y = 0f;
            var parent = layer.Parent;
            while(parent is not null)
            {
                if (parent.Source is not IImageStyle s)
                {
                    parent = parent.Parent;
                    continue;
                }
                if (s.Width > 0 && s.Height > 0)
                {
                    return (s.Width - x, s.Height - y);
                }
                x += s.X;
                y += s.Y;
                parent = parent.Parent;
            }
            var size = Instance!.Size;
            return (size.Width - x, size.Height - y);
        }

        private void TapLayerHorizontalRight(object? arg)
        {
            var layer = arg is IImageLayer o ? o : SelectedItem;
            if (layer is null)
            {
                return;
            }
            var (width, _) = GetParentSize(layer);
            if (layer.Source is IImageStyle s)
            {
                s.X = width - s.Width;
            }
            
        }

        private void TapLayerVerticalTop(object? arg)
        {
            var layer = arg is IImageLayer o ? o : SelectedItem;
            if (layer is null)
            {
                return;
            }
            if (layer.Source is IImageStyle s)
            {
                s.Y = 0;
            }
            
        }

        private void TapLayerVerticalMid(object? arg)
        {
            var layer = arg is IImageLayer o ? o : SelectedItem;
            if (layer is null)
            {
                return;
            }
            var (_, height) = GetParentSize(layer);
            if (layer.Source is IImageStyle s)
            {
                s.Y = (height - s.Height) / 2;
            }
            
        }

        private void TapLayerVerticalBottom(object? arg)
        {
            var layer = arg is IImageLayer o ? o : SelectedItem;
            if (layer is null)
            {
                return;
            }
            var (_, height) = GetParentSize(layer);
            if (layer.Source is IImageStyle s)
            {
                s.Y = height - s.Height;
            }
        }

        private void TapLayerHorizontalFlip(object? arg)
        {
            var layer = arg is IImageLayer o ? o : SelectedItem;
            if (layer is null)
            {
                return;
            }
            if (layer.Source is IImageStyle s)
            {
                s.ScaleX *= -1;
            }
        }

        private void TapLayerVerticalFlip(object? arg)
        {
            var layer = arg is IImageLayer o ? o : SelectedItem;
            if (layer is null)
            {
                return;
            }
            if (layer.Source is IImageStyle s)
            {
               s.ScaleY *= -1;
            }
            
        }

        protected override void TapLayerMoveTop(object? arg)
        {
            var layer = arg is IImageLayer o ? o : SelectedItem;
            if (layer is null)
            {
                return;
            }
            var items = layer.Parent is null ? LayerItems : layer.Parent.Children;
            items.MoveToFirst(items.IndexOf(layer));
        }

        protected override void TapLayerMoveUp(object? arg)
        {
            var layer = arg is IImageLayer o ? o : SelectedItem;
            if (layer is null)
            {
                return;
            }
            var items = layer.Parent is null ? LayerItems : layer.Parent.Children;
            items.MoveUp(items.IndexOf(layer));
        }

        protected override void TapLayerMoveDown(object? arg)
        {
            var layer = arg is IImageLayer o ? o : SelectedItem;
            if (layer is null)
            {
                return;
            }
            var items = layer.Parent is null ? LayerItems : layer.Parent.Children;
            items.MoveDown(items.IndexOf(layer));
        }
        protected override void TapLayerMoveBottom(object? arg)
        {
            var layer = arg is IImageLayer o ? o : SelectedItem;
            if (layer is null)
            {
                return;
            }
            var items = layer.Parent is null ? LayerItems : layer.Parent.Children;
            items.MoveToLast(items.IndexOf(layer));
        }

        private void TapLayerMoveParent(object? arg)
        {
            var layer = arg is IImageLayer o ? o : SelectedItem;
            if (layer is null)
            {
                return;
            }
            if (layer.Parent is null)
            {
                return;
            }
            if (layer.Source is IImageStyle s && layer.Parent.Source is IImageStyle ps)
            {
                s.X += ps.X;
                s.Y += ps.Y;
                s.Rotate += ps.Rotate;
                s.ScaleX *= ps.ScaleX;
                s.ScaleY *= ps.ScaleY;
            }
            
            layer.Parent.Children.Remove(layer);
            Instance?.Layer.InsertAfter([layer], layer.Parent);
        }
    }
}
