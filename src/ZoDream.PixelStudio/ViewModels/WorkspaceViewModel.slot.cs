using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZoDream.Shared.Interfaces;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class WorkspaceViewModel
    {


        private ObservableCollection<SlotItemViewModel> _slotItems = [];

        public ObservableCollection<SlotItemViewModel> SlotItems {
            get => _slotItems;
            set => SetProperty(ref _slotItems, value);
        }


        private void AddSlot(IEnumerable<ISkeletonSlot> items)
        {
            foreach (var item in items)
            {
                SlotItems.Add(new SlotItemViewModel()
                {
                    Name = item.Name,
                });
            }
        }
    }
}
