using System;
using ZoDream.Shared.ImageEditor;
using ZoDream.Shared.Interfaces;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class CreatorViewModel : IImageController
    {

        private readonly ImageStyleManager _styleManager = [];
        public IImageStyler Styler => _styleManager.TryGet(_styleMode, out var s) ? s : DefaultStyler;

        public IImageStyler DefaultStyler => _styleManager.Default;

        public IImageStyler RealStyler => _styleManager.Real;

        private string _styleMode;

        public string StyleMode {
            get => _styleMode;
            set {
                SetProperty(ref _styleMode, value);
                Instance?.Invalidate();
            }
        }
    }
}
