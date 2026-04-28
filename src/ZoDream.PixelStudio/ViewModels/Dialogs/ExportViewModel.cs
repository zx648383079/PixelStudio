using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using ZoDream.Shared.Interfaces;
using ZoDream.PixelStudio.Plugins;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ZoDream.PixelStudio.ViewModels
{
    public class ExportViewModel: ObservableObject
    {
        private string[] sourceItems = ["合并文件", "拆分文件", "选中层"];

        public string[] SourceItems {
            get => sourceItems;
            set => SetProperty(ref sourceItems, value);
        }

        private string[] typeItems = ["PNG", "Unity", "Godot", "Egret", "TexturePacker", "Spine", "MGCB"];

        public string[] TypeItems {
            get => typeItems;
            set => SetProperty(ref typeItems, value);
        }

        private int sourceIndex;

        public int SourceIndex {
            get => sourceIndex;
            set => SetProperty(ref sourceIndex, value);
        }

        private int typeIndex;

        public int TypeIndex {
            get => typeIndex;
            set => SetProperty(ref typeIndex, value);
        }

        private bool resetRotate;

        public bool ResetRotate {
            get => resetRotate;
            set => SetProperty(ref resetRotate, value);
        }

        private bool layerFolder;

        public bool LayerFolder {
            get => layerFolder;
            set => SetProperty(ref layerFolder, value);
        }


        public async Task<IStorageItem> OpenPickerAsync()
        {
            if (SourceIndex < 1)
            {
                var picker = new FileSavePicker();
                picker.FileTypeChoices.Add("Image", [".png", ".jpg", 
                    ".jpeg", ".webp", ".bmp"]);
                picker.FileTypeChoices.Add("KTX", [".ktx"]);
                picker.FileTypeChoices.Add("Windows ICO", [".ico"]);
                picker.FileTypeChoices.Add("Unity", [".asset"]);
                picker.FileTypeChoices.Add("Godot", [".tres"]);
                picker.FileTypeChoices.Add("Spine", [".atlas"]);
                picker.FileTypeChoices.Add("TexturePacker", [".plist", ".json"]);
                picker.FileTypeChoices.Add("MGCB", [".xml"]);
                App.ViewModel.InitializePicker(picker);
                return await picker.PickSaveFileAsync();
            }
            var folder = new FolderPicker();
            App.ViewModel.InitializePicker(folder);
            return await folder.PickSingleFolderAsync();
        }

        public IPluginReader? CreateWriter(IStorageFile filePath)
        {
            return ReaderFactory.GetSpriteExtensionReader(filePath.FileType, filePath.Path);
        }
    }
}
