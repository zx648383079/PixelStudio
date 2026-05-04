using System.Collections.ObjectModel;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class CreatorViewModel
    {

        private readonly AppViewModel _app = App.ViewModel;

        private string _selectedMode = "Move";

        public string SelectedMode {
            get => _selectedMode;
            set {
                SetProperty(ref _selectedMode, value);
                OnPropertyChanged(nameof(SelectedModeIcon));
            }
        }

        public string SelectedModeIcon => SelectedMode switch
        {
            "Move" => "\uE8B0",
            "Pen" => "\uEDFB",
            "PenJoint" => "\uF003",
            _ => "\uF271",
        };

        private ObservableCollection<GlyphGroupViewModel> _glyphItems = [];

        public ObservableCollection<GlyphGroupViewModel> GlyphItems {
            get => _glyphItems;
            set => SetProperty(ref _glyphItems, value);
        }


        public bool IsSelectedLayer => false;
    }
}
