using BitmapToVector;
using BitmapToVector.SkiaSharp;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using ZoDream.PixelStudio.Dialogs;
using ZoDream.Shared.Font;
using ZoDream.Shared.ImageEditor.Sources;
using ZoDream.Shared.OpenType;
using ZoDream.Shared.WebType;

namespace ZoDream.PixelStudio.ViewModels
{
    public partial class CreatorViewModel
    {

        public ICommand AddGlyphCommand { get; set; }


        private async void TapAddGlyph()
        {
            var dialog = new CreateGlyphDialog();
            if (await _app.OpenDialogAsync(dialog) != Microsoft.UI.Xaml.Controls.ContentDialogResult.Primary)
            {
                return;
            }
            foreach (var item in dialog.Items)
            {
                AddGlyph(item);
            }
        }

        private void AddGlyph(uint value)
        {
            foreach (var item in GlyphItems)
            {
                if (item.Contains(value) && !item.Items.Where(i => i.Character == value).Any())
                {
                    item.Items.Add(new(this)
                    {
                        Character = value
                    });
                }
            }
        }

        public override async void DragFiles(IEnumerable<IStorageItem> items)
        {
            IsLoading = true;
            foreach (var item in items)
            {
                if (item is not IStorageFile file)
                {
                    continue;
                }
                if (file.ContentType.StartsWith("image/"))
                {
                    using var bitmap = SKBitmap.Decode(file.Path);
                    var paths = PotraceSkiaSharp.Trace(new PotraceParam(), bitmap);
                    foreach (var path in paths)
                    {
                        Add(new PathImageSource(path, Instance));
                    }
                }
                else if (IsSupport(file.FileType))
                {
                    using var reader = await OpenRead(file);
                    if (reader is null)
                    {
                        continue;
                    }
                    var data = reader.Read();
                }
            }
            Instance?.Invalidate();
            IsLoading = false;
        }

        public static bool IsSupport(string extension)
        {
            return extension is ".woff" or ".woff2" or ".ttf" or ".otf" or ".ttc" or ".otc";
        }

        public async Task<ITypefaceReader?> OpenRead(IStorageFile file)
        {
            if (!IsSupport(file.FileType))
            {
                return null;
            }
            var input = await file.OpenStreamForReadAsync();
            var buffer = new byte[4];
            input.ReadExactly(buffer);
            input.Seek(0, SeekOrigin.Begin);
            if (buffer.SequenceEqual(WOFF2Reader.Signature))
            {
                return new WOFF2Reader(input);
            }
            if (buffer.SequenceEqual(WOFFReader.Signature))
            {
                return new WOFFReader(input);
            }
            if (buffer.SequenceEqual(TTCReader.Signature))
            {
                return new TTCReader(input);
            }
            if (file.FileType is ".ttf")
            {
                return new TTFReader(input);
            }
            return new OTFReader(input);
        }
    }
}
