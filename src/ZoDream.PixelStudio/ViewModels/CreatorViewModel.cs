using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using ZoDream.Shared.ImageEditor;
using ZoDream.Shared.Interfaces;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class CreatorViewModel : ObservableObject, IDisposable
    {
        public CreatorViewModel()
        {
            ExitCommand = new RelayCommand(TapExit);

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
        }
    }
}
