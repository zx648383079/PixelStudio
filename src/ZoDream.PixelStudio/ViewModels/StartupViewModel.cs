using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Input;
using Windows.Storage.Pickers;
using ZoDream.PixelStudio.Pages;
using ZoDream.PixelStudio.Plugins;

namespace ZoDream.PixelStudio.ViewModels
{
    public class StartupViewModel : ObservableObject
    {
        public StartupViewModel()
        {
            OpenCommand = new RelayCommand(TapOpen);
            CreateCommand = new RelayCommand(TapCreate);
            CreateFontCommand = new RelayCommand(TapCreateFont);
            version = App.ViewModel.Version;
        }

        private string version;

        public string Version {
            get => version;
            set => SetProperty(ref version, value);
        }

        public ICommand OpenCommand { get; private set; }

        public ICommand CreateCommand { get; private set; }
        public ICommand CreateFontCommand { get; private set; }

        private async void TapOpen()
        {
            var picker = new FileOpenPicker();
            foreach (var ext in ReaderFactory.FileFilterItems)
            {
                picker.FileTypeFilter.Add(ext);
            }
            picker.FileTypeFilter.Add("*");
            App.ViewModel.InitializePicker(picker);
            var items = await picker.PickMultipleFilesAsync();
            if (items.Count == 0)
            {
                return;
            }
            App.ViewModel.Navigate<WorkspacePage>(items);
        }

        private void TapOpenFolder()
        {
            //var picker = new FolderPicker();
            //App.ViewModel.InitializePicker(picker);
            //picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            //var folder = await picker.PickSingleFolderAsync();
            //if (folder is null)
            //{
            //    return;
            //}
            //var checkFile = await folder.GetFileAsync(AppConstants.DatabaseFileName);
            //if (checkFile is null)
            //{
            //    // 不存在
            //    return;
            //}
            //StorageApplicationPermissions.FutureAccessList.AddOrReplace(AppConstants.WorkspaceToken, folder);
            //await App.GetService<AppViewModel>().InitializeWorkspaceAsync(folder);
            //App.GetService<IRouter>().GoToAsync(Router.HomeRoute);
        }

        private void TapCreate()
        {
            App.ViewModel.Navigate<WorkspacePage>();
        }

        private void TapCreateFont()
        {
            App.ViewModel.Navigate<FontCreatorPage>();
        }
    }
}
