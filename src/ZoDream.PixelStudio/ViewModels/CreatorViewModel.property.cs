using ZoDream.Shared.Interfaces;
using ZoDream.Shared.UndoRedo;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class CreatorViewModel
    {
        private readonly AppViewModel _app = App.ViewModel;

        public CommandManager UndoRedo { get; private set; } = new();
        public IImageEditor? Instance { get; set; }


        private bool _undoEnabled;

        public bool UndoEnabled {
            get => _undoEnabled;
            set => SetProperty(ref _undoEnabled, value);
        }

        private bool _redoEnabled;

        public bool RedoEnabled {
            get => _redoEnabled;
            set => SetProperty(ref _redoEnabled, value);
        }

        private bool _isLoading;

        public bool IsLoading {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

    }
}
