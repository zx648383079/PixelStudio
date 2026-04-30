using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System.Collections.Generic;
using Windows.Storage;
using ZoDream.PixelStudio.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ZoDream.PixelStudio.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WorkspacePage : Page
    {
        public WorkspacePage()
        {
            InitializeComponent();
        }

        public WorkspaceViewModel ViewModel => (WorkspaceViewModel)DataContext;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            App.ViewModel.BindMenu(ViewModel);
            ViewModel.Initialize(CanvasShell);
            if (e.Parameter is IEnumerable<IStorageItem> items)
            {
                ViewModel.DragFiles(items);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            ViewModel.Dispose();
        }
    }
}
