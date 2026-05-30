using System;
using System.Windows.Input;

namespace ZoDream.PixelStudio.ViewModels
{
    public class PluginMenuItem(string group, string name, Type instanceType)
    {

        public const string PaintName = "PAINT";
        public const string ImportName = "IMPORT";
        public const string ExportName = "EXPORT";
        public const string EditName = "";

        public PluginMenuItem(string name, Type instanceType)
            : this(string.Empty, name, instanceType)
        {
            
        }

        public string Name => name;

        public string Group => group;

        public Type InstanceType => instanceType;
        public ICommand? Command { get; private set; }
    }
}
