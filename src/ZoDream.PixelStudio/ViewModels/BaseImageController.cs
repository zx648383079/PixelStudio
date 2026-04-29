using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using Windows.Storage;
using ZoDream.Shared.Interfaces;
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


        public CommandManager UndoRedo { get; private set; } = new();
        public IImageEditor? Instance { get; set; }

        protected string FileName { get; set; } = string.Empty;


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

        private void UndoRedo_ReverseUndoStateChanged(bool value)
        {
            RedoEnabled = value;
        }

        private void UndoRedo_UndoStateChanged(bool value)
        {
            UndoEnabled = value;
        }
    }
}
