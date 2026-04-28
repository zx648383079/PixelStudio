using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI;
using SkiaSharp.Views.Windows;
using Windows.UI;
using ZoDream.Shared.ImageEditor.Sources;
using ZoDream.Shared.Interfaces;

namespace ZoDream.PixelStudio.ViewModels
{
    public class CreateRectDialogViewModel: ObservableObject, 
        IFormValidator, ILayerCreator
    {
        private float _x;

        public float X {
            get => _x;
            set => SetProperty(ref _x, value);
        }

        private float _y;

        public float Y {
            get => _y;
            set => SetProperty(ref _y, value);
        }

        private float _width;

        public float Width {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        private float _height;

        public float Height {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        private float _leftRadius;

        public float LeftRadius {
            get => _leftRadius;
            set => SetProperty(ref _leftRadius, value);
        }
        private float _topRadius;

        public float TopRadius {
            get => _topRadius;
            set => SetProperty(ref _topRadius, value);
        }
        private float _rightRadius;

        public float RightRadius {
            get => _rightRadius;
            set => SetProperty(ref _rightRadius, value);
        }
        private float _bottomRadius;

        public float BottomRadius {
            get => _bottomRadius;
            set => SetProperty(ref _bottomRadius, value);
        }

        private float _strokeWidth = 1;

        public float StrokeWidth {
            get => _strokeWidth;
            set => SetProperty(ref _strokeWidth, value);
        }

        private Color _strokeColor = Colors.Black;

        public Color StrokeColor {
            get => _strokeColor;
            set => SetProperty(ref _strokeColor, value);
        }
        private Color _fillColor = Colors.Black;

        public Color FillColor {
            get => _fillColor;
            set => SetProperty(ref _fillColor, value);
        }

        public bool IsValid => Width == 0 || Height == 0;

        public bool TryCreate(IImageEditor editor)
        {
            if (!IsValid)
            {
                return false;
            }
            editor.Add(new RectImageSource(editor)
            {
                X = X,
                Y = Y,
                Width = Width,
                Height = Height,
                FillColor = FillColor.ToSKColor(),
                StrokeColor = StrokeColor.ToSKColor(),
                StrokeWidth = StrokeWidth,
                LeftRadius = LeftRadius,
                TopRadius = TopRadius,
                RightRadius = RightRadius,
                BottomRadius = BottomRadius,
            });
            return true;
        }
    }
}
