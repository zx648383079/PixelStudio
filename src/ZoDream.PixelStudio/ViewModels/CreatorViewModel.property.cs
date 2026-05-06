using System.Collections.ObjectModel;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class CreatorViewModel
    {

        private readonly AppViewModel _app = App.ViewModel;



        private ObservableCollection<GlyphGroupViewModel> _glyphItems = [];

        public ObservableCollection<GlyphGroupViewModel> GlyphItems {
            get => _glyphItems;
            set => SetProperty(ref _glyphItems, value);
        }



    }
}
