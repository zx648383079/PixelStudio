using System.Windows.Input;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class WorkspaceViewModel
    {
        private PluginMenuItem[] _pluginMenuItems = [];

        public PluginMenuItem[] PluginMenuItems {
            get => _pluginMenuItems;
            set => SetProperty(ref _pluginMenuItems, value);
        }

        public ICommand ExitCommand { get; private set; }

        private void TapExit()
        {
            App.Current.Exit();
        }

    }
}
