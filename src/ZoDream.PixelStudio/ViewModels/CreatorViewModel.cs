using CommunityToolkit.Mvvm.Input;
using System;
using System.Reflection;
using ZoDream.Shared.Font;
using ZoDream.Shared.ImageEditor;
using ZoDream.Shared.ImageEditor.Layers;
using ZoDream.Shared.Interfaces;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class CreatorViewModel : BaseImageController, IDisposable
    {
        public CreatorViewModel()
            : base()
        {
            AddGlyphCommand = new RelayCommand(TapAddGlyph);
            PluginMenuItems = _app.Plugin.Get(PluginMenuItem.ImportName);
            var items = typeof(UnicodeRanges).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var item in items)
            {
                if (item.GetValue(null) is not NamedUnicodeRange val)
                {
                    continue;
                }
                GlyphItems.Add(new GlyphGroupViewModel(val));
            }
        }

        public void Initialize(IImageShell shell)
        {
            var service = new ImageService();
            service.Add<IImageController>(this);
            service.Add<ILayerController>(this);
            Instance = new ImageEditor(shell, this, this, service);
            service.Add(Instance);
            Instance.Initialize();
            Instance.BackBar.Add(new GlyphLayoutLayer(Instance));
            ModeCommand.Execute(SelectedMode);
        }


        public void Dispose()
        {
            Instance?.Dispose();
        }
    }
}
