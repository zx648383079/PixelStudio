using CommunityToolkit.Mvvm.ComponentModel;

namespace ZoDream.PixelStudio.ViewModels
{
    public class ResourceItemViewModel : ObservableObject
    {

        public string Name { get; set; } = string.Empty;

        public string FullPath { get; set; } = string.Empty;
    }
}
