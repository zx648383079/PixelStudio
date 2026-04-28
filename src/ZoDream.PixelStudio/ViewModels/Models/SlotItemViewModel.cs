using CommunityToolkit.Mvvm.ComponentModel;

namespace ZoDream.PixelStudio.ViewModels
{
    public class SlotItemViewModel : ObservableObject
    {


        public string Name { get; set; } = string.Empty;

        public int FrameCount { get; set; }

        private int _frameIndex;

        public int FrameIndex {
            get => _frameIndex;
            set => SetProperty(ref _frameIndex, value);
        }

    }
}
