using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;
using System.Windows.Input;
using ZoDream.Shared.Drawing;
using ZoDream.PixelStudio.Dialogs;
using ZoDream.PixelStudio.Plugins;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.ImageEditor.Sources;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class WorkspaceViewModel
    {
        public ICommand EditorSelectedCommand { get; private set; }
        public ICommand LayerSelectedCommand { get; private set; }


        public ICommand SeparateCommand { get; private set; }
        public ICommand OrderCommand { get; private set; }
       
        public ICommand AddGroupCommand { get; private set; }
        public ICommand UngroupCommand { get; private set; }


        public ICommand ImportFolderCommand { get; private set; }
        public ICommand SelectParentCommand { get; private set; }
        public ICommand LayerApplyCommand { get; private set; }


        public ICommand LayerPropertyCommand { get; private set; }
        public ICommand LayerRotateCommand { get; private set; }
        public ICommand LayerScaleCommand { get; private set; }
        public ICommand LayerScaleXCommand { get; private set; }
        public ICommand LayerScaleYCommand { get; private set; }
        
        public ICommand LayerHorizontalLeftCommand { get; private set; }
        public ICommand LayerHorizontalCenterCommand { get; private set; }
        public ICommand LayerHorizontalRightCommand { get; private set; }

        public ICommand LayerVerticalTopCommand { get; private set; }
        public ICommand LayerVerticalMidCommand { get; private set; }
        public ICommand LayerVerticalBottomCommand { get; private set; }
        public ICommand LayerHorizontalFlipCommand { get; private set; }
        public ICommand LayerVerticalFlipCommand { get; private set; }

        public ICommand LayerMoveParentCommand { get; private set; }


        private async void TapAddLayer(string? cmd)
        {
            ContentDialog dialog = cmd switch
            {
                "text" => new CreateTextDialog(),
                "rect" => new CreateRectDialog(),
                "circle" => new CreateCircleDialog(),
                "path" => new CreatePathDialog(),
                _ => new LayerDialog(),
            };
            var model = dialog.DataContext as ILayerCreator;
            var res = await App.ViewModel.OpenFormAsync(dialog);
            if (!res || model is null || !model.TryCreate(Instance!))
            {
                return;
            }
            Instance?.Invalidate();
        }

        private async void TapAddGroup()
        {
            var dialog = new GroupDialog();
            var res = await App.ViewModel.OpenFormAsync(dialog);
            if (!res)
            {
                return;
            }
            Instance?.AddFolder(dialog.ViewModel.Name);
        }

        



        private void TapNew()
        {
            LayerItems.Clear();
            Instance?.Clear();
            Instance?.Unselect();
            Instance?.Invalidate();
            
        }

        private void TapOpen()
        {
            TapNew();
            TapImportFile();
        }


        
        private async void TapProperty()
        {
            var dialog = new PropertyDialog();
            dialog.ViewModel.Load(Instance);
            await App.ViewModel.OpenDialogAsync(dialog);
        }
        private void TapUnselect()
        {
            Instance!.Unselect();
            SelectedLayer = null;
        }
        private void TapOrder(object? arg)
        {
            if (Enum.TryParse<CssSpritesAlgorithm>(arg as string, out var res))
            {
                // TODO 层级问题
                var size = new CssSprites(res)
                    .Compute([.. Instance!.LayerItems.Select(i => (IImageBound)i.Source)]);
                Instance!.Resize(size.X, size.Y);
                Instance.Invalidate();
            }
        }

 

        /// <summary>
        /// 自动分离物体对象
        /// </summary>
        /// <param name="_"></param>
        private async void TapSeparate(object? arg)
        {
            if (Instance is null || LayerItems.Count == 0)
            {
                return;
            }
            var cmd = arg is string str ? str : "Auto";
            var layer = arg is IImageLayer ai ? ai : SelectedLayer;
            if (layer is null)
            {
                foreach (var item in LayerItems)
                {
                    if (!item.IsVisible || item.IsLocked || item.Children.Count > 0)
                    {
                        continue;
                    }
                    if (item.Source is not BitmapImageSource)
                    {
                        continue;
                    }
                    layer = item;
                    break;
                }
            }
            if(layer is null || layer.Source is not BitmapImageSource image)
            {
                return;
            }
            var dialog = new SeparateDialog();
            var model = dialog.ViewModel;
            model.SelectedIndex = cmd switch
            {
                "Count" => 1,
                "Size" => 2,
                "Rect" => 3,
                _ => 0,
            };
            if (!await App.ViewModel.OpenFormAsync(dialog))
            {
                return;
            }
            IsLoading = true;
            var partItems = await model.SplitAsync(image.Source);
            if (partItems.Length == 0)
            {
                IsLoading = false;
                return;
            }
            if (layer.Children.Count == 0 || !model.IsMerge)
            {
                SeparateImage(layer, partItems);
            } else
            {
                SeparateImageAndMerge(layer, partItems);
            }
            Instance.Invalidate();
            IsLoading = false;
        }

        

        private void OnEditorSelected(IImageLayer? layer)
        {
            SelectedLayer = layer;
        }

        private void OnLayerSelected(IImageLayer? layer)
        {
            if (layer is null)
            {
                return;
            }
            Instance?.Select(layer);
        }



        private void TapSelectTop()
        {
            if (LayerItems.Count == 0)
            {
                return;
            }
            var layer = SelectedLayer;
            if (layer is null || layer.Parent is null)
            {
                SelectedLayer = LayerItems[0];
            } else
            {
                SelectedLayer = layer.Parent.Children[0];
            }
            Instance?.Select(SelectedLayer);
        }

        private void TapSelectBottom()
        {
            if (LayerItems.Count == 0)
            {
                return;
            }
            var layer = SelectedLayer;
            if (layer is null || layer.Parent is null)
            {
                SelectedLayer = LayerItems.Last();
            }
            else
            {
                SelectedLayer = layer.Parent.Children.Last();
            }
            Instance?.Select(SelectedLayer);
        }

        private void TapSelectParent()
        {
            if (LayerItems.Count == 0 || SelectedLayer is null)
            {
                return;
            }
            var layer = SelectedLayer.Parent;
            if (layer is null)
            {
                return;
            }
            SelectedLayer = layer;
            Instance?.Select(layer);
        }

        private void TapSelectPrevious()
        {
            if (LayerItems.Count == 0 || SelectedLayer is null)
            {
                return;
            }
            var layer = SelectedLayer;
            if (layer.Parent is null)
            {
                SelectLayer(LayerItems.IndexOf(layer) - 1, LayerItems);
            } else
            {
                SelectLayer(layer.Parent.Children.IndexOf(layer) - 1, layer.Parent.Children);
            }
        }

        private void TapSelectNext()
        {
            if (LayerItems.Count == 0 || SelectedLayer is null)
            {
                return;
            }
            var layer = SelectedLayer;
            if (layer.Parent is null)
            {
                SelectLayer(LayerItems.IndexOf(layer) + 1, LayerItems);
            }
            else
            {
                SelectLayer(layer.Parent.Children.IndexOf(layer) + 1, layer.Parent.Children);
            }
        }

        private void SelectLayer(int i, IImageLayerTree items)
        {
            if (i < 0 || i >= items.Count)
            {
                return;
            }
            SelectedLayer = items[i];
            Instance?.Select(SelectedLayer);
        }
    }
}
