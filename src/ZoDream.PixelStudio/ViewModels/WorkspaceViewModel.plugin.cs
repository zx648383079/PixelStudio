using System;
using Windows.Storage.Pickers;
using ZoDream.Shared.Interfaces;
using ZoDream.PixelStudio.Plugins;
using ZoDream.Shared.ImageEditor;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class WorkspaceViewModel
    {
        protected override void TapPlugin(PluginMenuItem? plugin)
        {
            if (plugin is null)
            {
                return;
            }
            switch (plugin.Group)
            {
                case PluginMenuItem.ImportName:
                    TapImportPlugin(plugin);
                    break;
                case PluginMenuItem.ExportName:
                    TapExportPlugin(plugin);
                    break;
                case PluginMenuItem.PaintName:
                    TapPaintPlugin(plugin);
                    break;
                default:
                    break;
            }
        }

        private async void TapPaintPlugin(PluginMenuItem plugin)
        {
            if (Activator.CreateInstance(plugin.InstanceType) is not IPluginPainter painter)
            {
                return;
            }
            IsLoading = true;
            using var builder = new ImageBuilder(Instance!);
            await painter.PaintAsync(builder);
            foreach (var item in builder.Items)
            {
                Add(item);
            }
            builder.Items.Clear();
            Instance?.Invalidate();
            IsLoading = false;
        }

        private async void TapImportPlugin(PluginMenuItem plugin, string fileName = "")
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                var picker = new FileOpenPicker();
                foreach (var ext in ReaderFactory.FileFilterItems)
                {
                    picker.FileTypeFilter.Add(ext);
                }
                picker.FileTypeFilter.Add("*");
                _app.InitializePicker(picker);
                var res = await picker.PickSingleFileAsync();
                if (res is null)
                {
                    return;
                }
                fileName = res.Path;
            }
            IsLoading = true;
            var reader = Activator.CreateInstance(plugin.InstanceType);
            switch (reader)
            {
                case IPluginReader pr:
                    await foreach (var item in FileLoader.EnumerateLayer(pr, fileName))
                    {
                        await ImportSpriteAsync(item);
                    }
                    break;
            }
            Instance!.Resize();
            Instance.Invalidate();
            IsLoading = false;
        }


        private async void TapExportPlugin(PluginMenuItem plugin, string fileName = "")
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                var picker = new FileSavePicker();
                picker.FileTypeChoices.Add("All", [".*"]);
                App.ViewModel.InitializePicker(picker);
                var res = await picker.PickSaveFileAsync();
                if (res is null)
                {
                    return;
                }
                fileName = res.Path;
            }
            IsLoading = true;
            var writer = Activator.CreateInstance(plugin.InstanceType);
            if (writer is IPluginReader pw)
            {
                await pw.WriteAsync(fileName, [Serialize(fileName)]);
            }
            IsLoading = false;
        }
    }
}
