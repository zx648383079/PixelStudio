using System;
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
            
            PluginMenuItems = _app.Plugin.Get("import");
        }

        public void Initialize(IImageShell shell)
        {
            Instance = new ImageEditor(shell, this, this);
            Instance.Initialize();
            Instance.BackBar.Add(new GlyphLayoutLayer(Instance));
        }


        public void Dispose()
        {
            Instance?.Dispose();
        }
    }
}
