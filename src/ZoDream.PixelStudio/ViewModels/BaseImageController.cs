using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Media.Imaging;
using SkiaSharp;
using SkiaSharp.Views.Windows;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;
using ZoDream.Shared.UndoRedo;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class BaseImageController : ObservableObject
    {
        public BaseImageController()
        {
            NewCommand = new RelayCommand(TapNew);
            OpenCommand = new RelayCommand(TapOpen);
            SaveAsCommand = new RelayCommand(TapSaveAs);
            SaveCommand = new RelayCommand(TapSave);
            PluginCommand = new RelayCommand<PluginMenuItem>(TapPlugin);
            ImportCommand = new RelayCommand(TapImport);
            ExportCommand = new RelayCommand(TapExport);
            UndoCommand = new RelayCommand(TapUndo);
            RedoCommand = new RelayCommand(TapRedo);

            ModeCommand = new RelayCommand<string>(TapMode);
            SelectionChangedCommand = new RelayCommand<IReadOnlyList<object>>(OnSelectionChanged);

            CutCommand = new RelayCommand(TapCut);
            PasteCommand = new RelayCommand(TapPaste);
            CopyCommand = new RelayCommand(TapCopy);
            UnselectCommand = new RelayCommand(TapUnselect);
            PropertyCommand = new RelayCommand(TapProperty);
            TransparentCommand = new RelayCommand(TapTransparent);
            ExitCommand = new RelayCommand(TapExit);
            AboutCommand = new RelayCommand(TapAbout);
            AddLayerCommand = new RelayCommand<string>(TapAddLayer);
            DeleteLayerCommand = new RelayCommand<object>(TapDeleteLayer);
            SelectTopCommand = new RelayCommand(TapSelectTop);
            SelectBottomCommand = new RelayCommand(TapSelectBottom);
      
            SelectPreviousCommand = new RelayCommand(TapSelectPrevious);
            SelectNextCommand = new RelayCommand(TapSelectNext);
            LayerVisibleCommand = new RelayCommand<object>(TapLayerVisible);
            LayerVisibleToggleCommand = new RelayCommand<object>(TapLayerVisibleToggle);
            LayerLockToggleCommand = new RelayCommand<object>(TapLayerLockToggle);
            LayerHiddenCommand = new RelayCommand<object>(TapLayerHidden);
            AllVisibleCommand = new RelayCommand(TapAllVisible);
            OtherHiddenCommand = new RelayCommand<object>(TapOtherHidden);
            OtherVisibleCommand = new RelayCommand<object>(TapOtherVisible);
            LayerLockCommand = new RelayCommand<object>(TapLayerLock);
            LayerUnlockCommand = new RelayCommand<object>(TapLayerUnlock);
            AllUnlockCommand = new RelayCommand(TapAllUnlock);
            LayerRenameCommand = new RelayCommand<object>(TapLayerRename);
            LayerMoveTopCommand = new RelayCommand<object>(TapLayerMoveTop);
            LayerMoveUpCommand = new RelayCommand<object>(TapLayerMoveUp);
            LayerMoveDownCommand = new RelayCommand<object>(TapLayerMoveDown);
            LayerMoveBottomCommand = new RelayCommand<object>(TapLayerMoveBottom);
            LayerPropertyCommand = new RelayCommand<object>(TapLayerProperty);

            DragFileCommand = new RelayCommand<IReadOnlyList<IStorageItem>>(OnDragFiles);

            UndoRedo.UndoStateChanged += UndoRedo_UndoStateChanged;
            UndoRedo.ReverseUndoStateChanged += UndoRedo_ReverseUndoStateChanged;
        }

        private readonly Size _thumbnailSize = new(60, 60);
        
        public ICommandManager UndoRedo { get; private set; } = new CommandManager();
        public IImageEditor? Instance { get; set; }

        protected string FileName { get; set; } = string.Empty;

        private IImageLayer[] _selectedItems = [];

        public IImageLayer? SelectedItem {
            get => _selectedItems.FirstOrDefault();
            set {
                SelectedItems = value is null ? [] : [value];
            }
        }
        public IImageLayer[] SelectedItems { 
            get => _selectedItems; 
            set {
                foreach (var item in _selectedItems)
                {
                    item.IsSelected = false;
                }
                _selectedItems = value;
                foreach (var item in value)
                {
                    item.IsSelected = true;
                }
                OnPropertyChanged(nameof(IsSelectedLayer));
            }
        }


        private bool _undoEnabled;

        public bool UndoEnabled {
            get => _undoEnabled;
            set => SetProperty(ref _undoEnabled, value);
        }

        private bool _redoEnabled;

        public bool RedoEnabled {
            get => _redoEnabled;
            set => SetProperty(ref _redoEnabled, value);
        }

        private bool _isLoading;

        public bool IsLoading {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        private string _selectedMode = "Move";

        public string SelectedMode {
            get => _selectedMode;
            set {
                SetProperty(ref _selectedMode, value);
                OnPropertyChanged(nameof(SelectedModeIcon));
            }
        }

        public string SelectedModeIcon => SelectedMode switch
        {
            "Move" => "\uE8B0",
            "Pen" => "\uEDFB",
            "PenJoint" => "\uF003",
            _ => "\uF271",
        };

        public bool IsSelectedLayer => _selectedItems.Length > 0;

        public bool IsAdvanceMode => this is WorkspaceViewModel;


        private void UndoRedo_ReverseUndoStateChanged(bool value)
        {
            RedoEnabled = value;
        }

        private void UndoRedo_UndoStateChanged(bool value)
        {
            UndoEnabled = value;
        }

        public BitmapSource? CreateThumbnail(IImageSource source)
        {
            var data = source?.CreateThumbnail(_thumbnailSize);
            return (data as SKBitmap)?.ToWriteableBitmap();
        }
    }
}
