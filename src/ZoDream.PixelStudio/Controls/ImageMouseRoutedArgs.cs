using Microsoft.UI.Input;
using Microsoft.UI.Xaml.Input;
using Windows.System;
using ZoDream.Shared;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.PixelStudio.Controls
{
    public class ImageMouseRoutedArgs(Point point, 
        PointerState state,
        PointerPoint? source = null, VirtualKeyModifiers? modifiers = null) : IMouseRoutedArgs
    {
        public Point Position => point;

        public PointerState State => state;

        public bool IsLeftButtonPressed => source?.Properties?.IsLeftButtonPressed == true;

        public bool IsRightButtonPressed => source?.Properties?.IsRightButtonPressed == true;

        public bool IsControlPressed => modifiers?.HasFlag(VirtualKeyModifiers.Control) == true;

        public bool IsShiftPressed => modifiers?.HasFlag(VirtualKeyModifiers.Shift) == true;
    }

    public class ImageKeyboardRoutedArgs(KeyRoutedEventArgs args) : IKeyboardRoutedArgs
    {
        public bool IsControl => args.Key == VirtualKey.Control;

        public bool IsShift => args.Key == VirtualKey.Shift;
    }
}
