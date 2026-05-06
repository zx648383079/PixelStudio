using ZoDream.Shared.Interfaces;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class WorkspaceViewModel
    {

        private readonly AppViewModel _app = App.ViewModel;



        private IImageLayer? _selectedLayer;

        public IImageLayer? SelectedLayer {
            get => _selectedLayer;
            set {
                SetProperty(ref _selectedLayer, value);
                IsSelectedLayer = value != null;
            }
        }
    }
}
