using CommunityToolkit.Mvvm.Input;
using System;
using ZoDream.Shared.ImageEditor;
using ZoDream.Shared.Interfaces;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class WorkspaceViewModel : BaseImageController, IDisposable, IImageController
    {
        public WorkspaceViewModel()
        {
            
            SeparateCommand = new RelayCommand<object?>(TapSeparate);
            OrderCommand = new RelayCommand<object?>(TapOrder);
            
            AddGroupCommand = new RelayCommand(TapAddGroup);
            UngroupCommand = new RelayCommand<IImageLayer?>(TapUngroup);
            
            ImportFolderCommand = new RelayCommand(TapImportFolder);
       
            SelectParentCommand = new RelayCommand(TapSelectParent);

            LayerApplyCommand = new RelayCommand<IImageLayer>(TapLayerApply);
            LayerRotateCommand = new RelayCommand<object?>(TapLayerRotate);
            LayerScaleCommand = new RelayCommand(TapLayerScale);
            LayerScaleXCommand = new RelayCommand(TapLayerScaleX);
            LayerScaleYCommand = new RelayCommand(TapLayerScaleY);

    
            LayerHorizontalLeftCommand = new RelayCommand<object?>(TapLayerHorizontalLeft);
            LayerHorizontalCenterCommand = new RelayCommand<object?>(TapLayerHorizontalCenter);
            LayerHorizontalRightCommand = new RelayCommand<object?>(TapLayerHorizontalRight);
            LayerVerticalTopCommand = new RelayCommand<object?>(TapLayerVerticalTop);
            LayerVerticalMidCommand = new RelayCommand<object?>(TapLayerVerticalMid);
            LayerVerticalBottomCommand = new RelayCommand<object?>(TapLayerVerticalBottom);
            LayerHorizontalFlipCommand = new RelayCommand<object?>(TapLayerHorizontalFlip);
            LayerVerticalFlipCommand = new RelayCommand<object?>(TapLayerVerticalFlip);
            
            LayerMoveParentCommand = new RelayCommand<object?>(TapLayerMoveParent);
            LayerSelectedCommand = new RelayCommand<IImageLayer>(OnLayerSelected);
            EditorSelectedCommand = new RelayCommand<IImageLayer>(OnEditorSelected);
            UndoRedo.UndoStateChanged += UndoRedo_UndoStateChanged;
            UndoRedo.ReverseUndoStateChanged += UndoRedo_ReverseUndoStateChanged;
            PluginMenuItems = _app.Plugin.Get("import");
        }


        public void Initialize(IImageShell shell)
        {
            var service = new ImageService();
            service.Add<IImageController>(this);
            service.Add<ILayerController>(this);
            Instance = new ImageEditor(shell, this, this, service);
            service.Add(Instance);
            Instance.Initialize();
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
