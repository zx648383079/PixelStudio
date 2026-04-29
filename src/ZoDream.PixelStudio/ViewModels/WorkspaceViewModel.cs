using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using Windows.Storage;
using ZoDream.Shared.ImageEditor;
using ZoDream.Shared.Interfaces;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class WorkspaceViewModel : ObservableObject, IDisposable, IImageController
    {
        public WorkspaceViewModel()
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
            SeparateCommand = new RelayCommand<object?>(TapSeparate);
            OrderCommand = new RelayCommand<object?>(TapOrder);
            AboutCommand = new RelayCommand(TapAbout);
            AddLayerCommand = new RelayCommand<string>(TapAddLayer);
            AddGroupCommand = new RelayCommand(TapAddGroup);
            UngroupCommand = new RelayCommand<IImageLayer?>(TapUngroup);
            DeleteLayerCommand = new RelayCommand<object?>(TapDeleteLayer);
            ImportFolderCommand = new RelayCommand(TapImportFolder);
            LayerPropertyCommand = new RelayCommand<object?>(TapLayerProperty);
            SelectTopCommand = new RelayCommand(TapSelectTop);
            SelectBottomCommand = new RelayCommand(TapSelectBottom);
            SelectParentCommand = new RelayCommand(TapSelectParent);
            SelectPreviousCommand = new RelayCommand(TapSelectPrevious);
            SelectNextCommand = new RelayCommand(TapSelectNext);
            LayerApplyCommand = new RelayCommand<IImageLayer>(TapLayerApply);
            LayerRotateCommand = new RelayCommand<object?>(TapLayerRotate);
            LayerScaleCommand = new RelayCommand(TapLayerScale);
            LayerScaleXCommand = new RelayCommand(TapLayerScaleX);
            LayerScaleYCommand = new RelayCommand(TapLayerScaleY);
            LayerVisibleCommand = new RelayCommand<object?>(TapLayerVisible);
            LayerVisibleToggleCommand = new RelayCommand<object?>(TapLayerVisibleToggle);
            LayerLockToggleCommand = new RelayCommand<object?>(TapLayerLockToggle);
            LayerHiddenCommand = new RelayCommand<object?>(TapLayerHidden);
            AllVisibleCommand = new RelayCommand(TapAllVisible);
            OtherHiddenCommand = new RelayCommand<object?>(TapOtherHidden);
            OtherVisibleCommand = new RelayCommand<object?>(TapOtherVisible);
            LayerLockCommand = new RelayCommand<object?>(TapLayerLock);
            LayerUnlockCommand = new RelayCommand<object?>(TapLayerUnlock);
            AllUnlockCommand = new RelayCommand(TapAllUnlock);
            LayerRenameCommand = new RelayCommand<object?>(TapLayerRename);
            LayerHorizontalLeftCommand = new RelayCommand<object?>(TapLayerHorizontalLeft);
            LayerHorizontalCenterCommand = new RelayCommand<object?>(TapLayerHorizontalCenter);
            LayerHorizontalRightCommand = new RelayCommand<object?>(TapLayerHorizontalRight);
            LayerVerticalTopCommand = new RelayCommand<object?>(TapLayerVerticalTop);
            LayerVerticalMidCommand = new RelayCommand<object?>(TapLayerVerticalMid);
            LayerVerticalBottomCommand = new RelayCommand<object?>(TapLayerVerticalBottom);
            LayerHorizontalFlipCommand = new RelayCommand<object?>(TapLayerHorizontalFlip);
            LayerVerticalFlipCommand = new RelayCommand<object?>(TapLayerVerticalFlip);
            LayerMoveTopCommand = new RelayCommand<object?>(TapLayerMoveTop);
            LayerMoveUpCommand = new RelayCommand<object?>(TapLayerMoveUp);
            LayerMoveDownCommand = new RelayCommand<object?>(TapLayerMoveDown);
            LayerMoveBottomCommand = new RelayCommand<object?>(TapLayerMoveBottom);
            LayerMoveParentCommand = new RelayCommand<object?>(TapLayerMoveParent);
            ExitCommand = new RelayCommand(TapExit);
            LayerSelectedCommand = new RelayCommand<IImageLayer>(OnLayerSelected);
            EditorSelectedCommand = new RelayCommand<IImageLayer>(OnEditorSelected);
            DragImageCommand = new RelayCommand<IReadOnlyList<IStorageItem>>(OnDragImage);
            UndoRedo.UndoStateChanged += UndoRedo_UndoStateChanged;
            UndoRedo.ReverseUndoStateChanged += UndoRedo_ReverseUndoStateChanged;

            PluginMenuItems = _app.Plugin.Get("import");
        }


        public void Initialize(IImageShell shell)
        {
            Instance = new ImageEditor(shell, this, this);
        }
        private void UndoRedo_ReverseUndoStateChanged(bool value)
        {
            RedoEnabled = value;
        }

        private void UndoRedo_UndoStateChanged(bool value)
        {
            UndoEnabled = value;
        }

        public void Dispose()
        {
            Instance?.Dispose();
            _styleManager.Dispose();
        }
    }
}
