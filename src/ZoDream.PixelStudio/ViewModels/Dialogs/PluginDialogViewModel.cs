using System;
using System.Windows.Input;
using Windows.Storage.Pickers;
using ZoDream.PixelStudio.Plugins;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ZoDream.PixelStudio.ViewModels
{
    internal class PluginDialogViewModel : ObservableObject
    {
        public PluginDialogViewModel()
        {
            OpenCommand = new RelayCommand(TapOpen);
            OutputCommand = new RelayCommand(TapOutput);
        }

        private string _fileName = string.Empty;

		public string FileName {
			get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        private string _outputFileName = string.Empty;

        public string OutputFileName {
            get => _outputFileName;
            set => SetProperty(ref _outputFileName, value);
        }


        public ICommand OpenCommand { get; private set; }
        public ICommand OutputCommand { get; private set; }

        private async void TapOpen()
        {
            var picker = new FileOpenPicker();
            foreach (var ext in ReaderFactory.FileFilterItems)
            {
                picker.FileTypeFilter.Add(ext);
            }
            picker.FileTypeFilter.Add("*");
            App.ViewModel.InitializePicker(picker);
            var res = await picker.PickSingleFileAsync();
            if (res is null)
            {
                return;
            }
            FileName = res.Path;
        }

        private async void TapOutput()
        {
            var picker = new FileSavePicker();
            picker.FileTypeChoices.Add("All", [".*"]);
            picker.SuggestedFileName = "undefined.json";
            App.ViewModel.InitializePicker(picker);
            var res = await picker.PickSaveFileAsync();
            if (res is null)
            {
                return;
            }
            OutputFileName = res.Path;
        }
    }
}
