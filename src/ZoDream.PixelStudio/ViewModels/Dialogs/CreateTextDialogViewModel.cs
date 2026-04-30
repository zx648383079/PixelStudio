using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI;
using SkiaSharp;
using SkiaSharp.Views.Windows;
using System.Linq;
using System.Windows.Input;
using Windows.UI;
using ZoDream.Shared.ImageEditor.Sources;
using ZoDream.Shared.Interfaces;

namespace ZoDream.PixelStudio.ViewModels
{
    public class CreateTextDialogViewModel: ObservableObject, 
        IFormValidator, ILayerCreator
    {
        public CreateTextDialogViewModel()
        {
            FamilyName = SKTypeface.Default.FamilyName;
            TextChangedCommand = new RelayCommand<bool>(OnTextChanged);
        }

        private string _text = string.Empty;

        public string Text {
            get => _text;
            set {
                SetProperty(ref _text, value);
                IsValid = !string.IsNullOrWhiteSpace(value);
            }
        }

        private int _size = 16;

        public int Size {
            get => _size;
            set => SetProperty(ref _size, value);
        }

        public string[] FamilyItems { get; set; } = SKFontManager.Default.FontFamilies.ToArray();

        private string _familyName = string.Empty;

        public string FamilyName {
            get => _familyName;
            set => SetProperty(ref _familyName, value);
        }

        private Color _foreground = Colors.Black;

        public Color Foreground {
            get => _foreground;
            set => SetProperty(ref _foreground, value);
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

        public bool TryCreate(IImageEditor editor)
        {
            if (!IsValid)
            {
                return false;
            }
            editor.Layer.Add(new TextImageSource(Text, editor)
            {
                FontFamily = SKFontManager.Default.MatchFamily(FamilyName),
                FontSize = Size,
                Color = Foreground.ToSKColor()
            });
            return true;
        }
    }
}
