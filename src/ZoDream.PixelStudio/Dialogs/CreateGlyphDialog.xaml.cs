using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ZoDream.PixelStudio.Dialogs
{
    public sealed partial class CreateGlyphDialog : ContentDialog
    {
        public CreateGlyphDialog()
        {
            InitializeComponent();
        }

        public IEnumerable<uint> Items {
            get {
                var text = tb.Text.Trim();
                foreach (var item in text.Split(',', ';'))
                {
                    var args = item.Split('-').Select(Parse).ToArray();
                    if (args.Length == 1 || args[1] <= args[0])
                    {
                        yield return args[0];
                        continue;
                    }
                    var count = args[1] - args[0];
                    for (uint i = args[0]; i <= count; i++)
                    {
                        yield return i;
                    }
                }
            }
        }

        private static uint Parse(string text)
        {
            text = text.Trim();
            if (text.Length == 0)
            {
                return 0;
            }
            if (text.StartsWith("0x"))
            {
                return Convert.ToUInt32(text, 16);
            }
            if (Regex.IsMatch(text, @"^\d+$"))
            {
                return uint.TryParse(text, out var v) ? v : 0; 
            }
            return text[0];
        }
    }
}
