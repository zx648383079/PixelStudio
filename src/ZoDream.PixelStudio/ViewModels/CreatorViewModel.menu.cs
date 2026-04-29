using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Windows.Input;
using ZoDream.Shared.ImageEditor;
using ZoDream.Shared.Interfaces;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class CreatorViewModel : IMenuController
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
