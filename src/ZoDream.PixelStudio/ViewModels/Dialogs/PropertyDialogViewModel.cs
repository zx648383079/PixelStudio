using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI;
using System.Windows.Input;
using Windows.UI;
using ZoDream.Shared.Interfaces;

namespace ZoDream.PixelStudio.ViewModels
{
    public class PropertyDialogViewModel: ObservableObject
    {
        public PropertyDialogViewModel()
        {
            SizeRestoreCommand = new RelayCommand(TapSizeRestore);
        }

        private string _name = string.Empty;

        public string Name {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private bool _isAutoSize;

        public bool IsAutoSize {
            get => _isAutoSize;
            set {
                SetProperty(ref _isAutoSize, value);
                OnPropertyChanged(nameof(IsSizeEnabled));
            }
        }

        public bool IsSizeEnabled => !IsAutoSize;

        private bool _isLockSize;

        public bool IsLockSize {
            get => _isLockSize;
            set => SetProperty(ref _isLockSize, value);
        }

        private int _width;

        public int Width {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        private int _height;

        public int Height {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        private bool _isEditableSize;

        public bool IsEditableSize {
            get => _isEditableSize;
            set => SetProperty(ref _isEditableSize, value);
        }

        private bool _isTransparentBackground = true;

        public bool IsTransparentBackground {
            get => _isTransparentBackground;
            set => SetProperty(ref _isTransparentBackground, value);
        }

        private Color _foreground = Colors.White;

        public Color Foreground {
            get => _foreground;
            set => SetProperty(ref _foreground, value);
        }

        private Color _background = Colors.Black;

        public Color Background {
            get => _background;
            set => SetProperty(ref _background, value);
        }

        public bool SizeRestoreEnabled => !IsEditableSize;

        public ICommand SizeRestoreCommand { get; private set; }

        private void TapSizeRestore()
        {
            IsEditableSize = false;
        }

        public void Load(IImageEditor editor)
        {
            //Name = layer.Name;
            var size = editor.Size;
            Width = (int)size.Width;
            Height = (int)size.Height;
        }

        public void Save(IImageEditor layer)
        {

        }
    }
}
