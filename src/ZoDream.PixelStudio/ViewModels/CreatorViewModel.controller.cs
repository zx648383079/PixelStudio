using CommunityToolkit.Mvvm.ComponentModel;
using System;
using ZoDream.Shared.ImageEditor;
using ZoDream.Shared.Interfaces;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class CreatorViewModel : IImageController
    {
        public IImageStyler Styler => throw new NotImplementedException();

        public IImageStyler DefaultStyler => throw new NotImplementedException();

        public IImageStyler RealStyler => throw new NotImplementedException();
    }
}
