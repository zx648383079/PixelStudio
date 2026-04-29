using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class AppViewModel
    {
        
        private Frame _rootFrame;

        private MenuBar? _menuBar;

        public void Navigate<T>() where T : Page
        {
            _rootFrame.Navigate(typeof(T));
        }

        public void Navigate<T>(object parameter) where T : Page
        {
            _rootFrame.Navigate(typeof(T), parameter);
        }

        internal void BindMenu(IMenuController controller)
        {
            if (_menuBar is null)
            {
                return;
            }
            _menuBar.Visibility = Visibility.Visible;
            _menuBar.DataContext = controller;
        }

   
    }
}
