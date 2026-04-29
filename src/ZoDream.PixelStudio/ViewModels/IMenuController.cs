using System;
using System.Collections.Generic;
using System.Text;

namespace ZoDream.PixelStudio.ViewModels
{
    public interface IMenuController
    {
        public PluginMenuItem[] PluginMenuItems { get; }
    }
}
