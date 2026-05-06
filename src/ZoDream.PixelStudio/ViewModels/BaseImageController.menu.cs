using System;
using System.Collections.Generic;
using System.Windows.Input;
using Windows.Storage;
using ZoDream.PixelStudio.Dialogs;
using ZoDream.Shared.ImageEditor.Controllers;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class BaseImageController : IMenuController
    {
        public ICommand NewCommand { get; private set; }
        public ICommand OpenCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand SaveAsCommand { get; private set; }
        public ICommand ImportCommand { get; private set; }
        public ICommand ExportCommand { get; private set; }
        public ICommand UndoCommand { get; private set; }
        public ICommand RedoCommand { get; private set; }
        public ICommand ModeCommand { get; private set; }

        public ICommand CutCommand { get; private set; }
        public ICommand CopyCommand { get; private set; }
        public ICommand PasteCommand { get; private set; }
        public ICommand TransparentCommand { get; private set; }

        public ICommand PluginCommand { get; private set; }
        public ICommand AboutCommand { get; private set; }

        public ICommand DragFileCommand { get; private set; }


        public ICommand PropertyCommand { get; private set; }
        public ICommand UnselectCommand { get; private set; }

        public ICommand SelectionChangedCommand { get; private set; }
        public ICommand SelectTopCommand { get; private set; }
        public ICommand SelectBottomCommand { get; private set; }

        public ICommand SelectPreviousCommand { get; private set; }
        public ICommand SelectNextCommand { get; private set; }


        public ICommand AddLayerCommand { get; private set; }
        public ICommand DeleteLayerCommand { get; private set; }
        public ICommand LayerPropertyCommand { get; private set; }
        public ICommand LayerVisibleCommand { get; private set; }
        public ICommand LayerVisibleToggleCommand { get; private set; }
        public ICommand LayerHiddenCommand { get; private set; }
        public ICommand AllVisibleCommand { get; private set; }
        public ICommand OtherHiddenCommand { get; private set; }
        public ICommand OtherVisibleCommand { get; private set; }
        public ICommand LayerLockCommand { get; private set; }
        public ICommand LayerLockToggleCommand { get; private set; }
        public ICommand LayerUnlockCommand { get; private set; }
        public ICommand AllUnlockCommand { get; private set; }
        public ICommand LayerRenameCommand { get; private set; }
        public ICommand LayerMoveTopCommand { get; private set; }
        public ICommand LayerMoveUpCommand { get; private set; }
        public ICommand LayerMoveDownCommand { get; private set; }
        public ICommand LayerMoveBottomCommand { get; private set; }


        private PluginMenuItem[] _pluginMenuItems = [];

        public PluginMenuItem[] PluginMenuItems {
            get => _pluginMenuItems;
            set => SetProperty(ref _pluginMenuItems, value);
        }

        public ICommand ExitCommand { get; private set; }

        private void TapExit()
        {
            App.Current.Exit();
        }

        private async void TapAbout()
        {
            var dialog = new AboutDialog();
            await App.ViewModel.OpenDialogAsync(dialog);
        }

        private void TapMode(string? mode)
        {
            SelectedMode = mode ?? string.Empty;
            switch (mode)
            {
                case "Move":
                    Instance.SwitchMode<MoveController>();
                    break;
                case "Pen":
                    Instance.SwitchMode<PenController>();
                    break;
                case "PenJoint":
                    Instance.SwitchMode<PenJointController>();
                    break;
                default:
                    Instance.SwitchMode<ViewController>();
                    break;
            }
        }

        protected virtual void TapNew()
        {

        }
        protected virtual void TapOpen()
        {

        }

        private void OnSelectionChanged(IReadOnlyList<object>? items)
        {
            if (items is null)
            {
                return;
            }
            var first = items.Count > 0 ? items[0] : null;
            if (first is IImageLayer layer)
            {
                SelectedItem = layer;
                Instance!.Select(layer);
            }
        }

        private void TapSaveAs()
        {
            TapSaveAs(string.Empty);
        }
        protected virtual void TapSaveAs(string fileName)
        {

        }
        private void TapSave()
        {
            TapSaveAs(FileName);
        }
        protected virtual void TapPlugin(PluginMenuItem? plugin)
        {

        }
        protected virtual void TapImport()
        {

        }
        protected virtual void TapExport()
        {

        }
        private void TapUndo()
        {
            UndoRedo.Undo();
        }
        private void TapRedo()
        {
            UndoRedo.ReverseUndo();
        }

        protected virtual void TapCut()
        {

        }
        protected virtual void TapPaste()
        {

        }
        protected virtual void TapCopy()
        {

        }
        protected virtual void TapUnselect()
        {
            SelectedItems = [];
            Instance!.Unselect();
        }
        protected virtual void TapProperty()
        {

        }
        private void TapTransparent()
        {
            Instance!.BackgroundColor = Instance.BackgroundColor is null ? Color.White : null;
            Instance.Invalidate();
        }

        protected virtual void TapAddLayer(string? text)
        {

        }
        protected virtual void TapDeleteLayer(object? arg)
        {

        }
        protected virtual void TapSelectTop()
        {

        }
        protected virtual void TapSelectBottom()
        {

        }
        protected virtual void TapSelectPrevious()
        {

        }
        protected virtual void TapSelectNext()
        {

        }
        protected virtual void TapLayerVisible(object? arg)
        {

        }
        protected virtual void TapLayerVisibleToggle(object? arg)
        {

        }
        protected virtual void TapLayerLockToggle(object? arg)
        {

        }
        protected virtual void TapLayerHidden(object? arg)
        {

        }
        protected virtual void TapAllVisible()
        {

        }
        protected virtual void TapOtherHidden(object? arg)
        {

        }
        protected virtual void TapOtherVisible(object? arg)
        {

        }
        protected virtual void TapLayerLock(object? arg)
        {

        }
        protected virtual void TapLayerUnlock(object? arg)
        {

        }
        protected virtual void TapAllUnlock()
        {

        }
        protected virtual void TapLayerRename(object? arg)
        {

        }
        protected virtual void TapLayerMoveTop(object? arg)
        {

        }
        protected virtual void TapLayerMoveUp(object? arg)
        {

        }
        protected virtual void TapLayerMoveDown(object? arg)
        {

        }
        protected virtual void TapLayerMoveBottom(object? arg)
        {

        }

        protected virtual void TapLayerProperty(object? arg)
        {

        }
        private void OnDragFiles(IReadOnlyList<IStorageItem>? items)
        {
            if (items is null)
            {
                return;
            }
            DragFiles(items);
        }

        public virtual void DragFiles(IEnumerable<IStorageItem> items)
        {

        }
    }
}
