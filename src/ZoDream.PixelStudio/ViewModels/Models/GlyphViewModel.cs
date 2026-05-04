using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using ZoDream.Shared.Font;
using ZoDream.Shared.Interfaces;

namespace ZoDream.PixelStudio.ViewModels
{
    public class GlyphViewModel(IImageController workspace) : ObservableObject
    {


        public IImageController Workspace => workspace;


        private uint _character;

        public uint Character { 
            get => _character; 
            set {
                _character = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Name => $"0x{Character:X4}";
    }

    public class GlyphGroupViewModel : ObservableObject
    {

        public GlyphGroupViewModel()
        {
            
        }

        public GlyphGroupViewModel(NamedUnicodeRange range)
        {
            Name = $"{range.Name}(0x{range.Start:X4}-0x{range.End:X4})";
            _beginRange = range.Start;
            _endRange = range.End;
        }

        private readonly uint _beginRange;
        private readonly uint _endRange;

        private string _name = string.Empty;

        public string Name {
            get => _name;
            set => SetProperty(ref _name, value);
        }



        private ObservableCollection<GlyphViewModel> _items = [];

        public ObservableCollection<GlyphViewModel> Items {
            get => _items;
            set => SetProperty(ref _items, value);
        }


        public bool Contains(uint index) => index >= _beginRange && index <= _endRange;
    }
}
