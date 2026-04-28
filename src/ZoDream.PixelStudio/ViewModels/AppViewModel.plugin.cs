using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;
using ZoDream.PixelStudio.Dialogs;
using ZoDream.PixelStudio.Plugins;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class AppViewModel
    {

        public readonly PluginCollection Plugin = [];


        public async Task<PluginMenuItem?> PickPlugin(string group)
        {
            var dialog = CreatePickPlugin(group);
            if (await OpenDialogAsync(dialog) == ContentDialogResult.Primary)
            {
                return dialog.SelectedItem;
            }
            return null;
        }

        public PluginDialog CreatePickPlugin(string group)
        {
            return new PluginDialog
            {
                Title = $"{Plugin.GetGroupName(group)} 插件选择",
                ItemsSource = Plugin.Get(group),
                FileNameEnabled = group == PluginMenuItem.ImportName,
                OutputEnabled = group == PluginMenuItem.ExportName
            };
        }
    }
}
