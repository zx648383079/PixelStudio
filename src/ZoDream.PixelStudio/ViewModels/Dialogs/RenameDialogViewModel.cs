using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace ZoDream.PixelStudio.ViewModels
{
    public class RenameDialogViewModel : ObservableObject, IFormValidator
    {
        public RenameDialogViewModel()
        {
            TextChangedCommand = new RelayCommand<bool>(OnTextChanged);
        }

        private string _name = string.Empty;

        public string Name {
            get => _name;
            set {
                SetProperty(ref _name, value);
                IsValid = !string.IsNullOrWhiteSpace(value);
            }
        }

        private bool _isValid;

        public bool IsValid {
            get => _isValid;
            set => SetProperty(ref _isValid, value);
        }


        public ICommand TextChangedCommand { get; private set; }

        private void OnTextChanged(bool changed)
        {
            IsValid = changed;
        }
    }
}
