using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ZoDream.Shared.Interfaces;

namespace ZoDream.PixelStudio.ViewModels
{
    public class LayerPropertyDialogViewModel: ObservableObject, IFormValidator
    {
        public LayerPropertyDialogViewModel()
        {
            OffsetRestoreCommand = new RelayCommand(TapOffsetRestore);
            ScaleRestoreCommand = new RelayCommand(TapScaleRestore);
            RotateRestoreCommand = new RelayCommand(TapRotateRestore);
        }

        private string _name = string.Empty;

        public string Name {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private bool _isLockSize;

        public bool IsLockSize {
            get => _isLockSize;
            set => SetProperty(ref _isLockSize, value);
        }

        private bool _isLockScale;

        public bool IsLockScale {
            get => _isLockScale;
            set => SetProperty(ref _isLockScale, value);
        }

        private int _x;

        public int X {
            get => _x;
            set {
                SetProperty(ref _x, value);
                OnPropertyChanged(nameof(OffsetRestoreEnabled));
            }
        }

        private int _y;

        public int Y {
            get => _y;
            set {
                SetProperty(ref _y, value);
                OnPropertyChanged(nameof(OffsetRestoreEnabled));
            }
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


        private double _scaleX = 1;

        public double ScaleX {
            get => _scaleX;
            set {
                SetProperty(ref _scaleX, value);
                OnPropertyChanged(nameof(ScaleRestoreEnabled));
            }
        }

        private double _scaleY = 1;

        public double ScaleY {
            get => _scaleY;
            set {
                SetProperty(ref _scaleY, value);
                OnPropertyChanged(nameof(ScaleRestoreEnabled));
            }
        }

        private double _rotate;

        public double Rotate {
            get => _rotate;
            set {
                SetProperty(ref _rotate, value);
                OnPropertyChanged(nameof(RotateRestoreEnabled));
            }
        }


        private bool _isVisible = true;

        public bool IsVisible {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }

        private bool _isLocked;

        public bool IsLocked {
            get => _isLocked;
            set => SetProperty(ref _isLocked, value);
        }

        public bool OffsetRestoreEnabled => X != 0 || Y != 0;
        public bool ScaleRestoreEnabled => ScaleX != 1 || ScaleY != 1;
        public bool RotateRestoreEnabled => Rotate != 0;

        public bool IsValid => true;

        public ICommand OffsetRestoreCommand { get; private set; }
        public ICommand ScaleRestoreCommand { get; private set; }
        public ICommand RotateRestoreCommand { get; private set; }

        private void TapOffsetRestore()
        {
            X = 0;
            Y = 0;
        }

        private void TapScaleRestore()
        {
            ScaleX = 1;
            ScaleY = 1;
        }
        private void TapRotateRestore()
        {
            Rotate = 0;
        }

        public void Load(IImageLayer layer)
        {
            Name = layer.Name;
            if (layer.Source is IImageStyleSource src)
            {
                X = (int)src.X;
                Y = (int)src.Y;
                Width = (int)src.Width;
                Height = (int)src.Height;
                ScaleX = src.ScaleX;
                ScaleY = src.ScaleY;
                Rotate = src.Rotate;
            }
            
            IsVisible = layer.IsVisible;
            IsLocked = layer.IsLocked;
        }

        public void Save(IImageLayer layer)
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                layer.Name = Name;
            }
            if (layer.Source is IImageStyleSource src)
            {
                src.X = X;
                src.Y = Y;
                if (Width > 0)
                {
                    src.Width = Width;
                }
                if (Height > 0)
                {
                    src.Height = Height;
                }

                if (ScaleX != 0)
                {
                    src.ScaleX = (float)ScaleX;
                }
                if (ScaleY != 0)
                {
                    src.ScaleY = (float)ScaleY;
                }
                src.Rotate = (float)Rotate;
            }
            
            layer.IsVisible = IsVisible;
            layer.IsLocked = IsLocked;
        }
    }
}
