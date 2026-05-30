using System.Collections.Generic;
using ZoDream.PixelStudio.ViewModels;
using ZoDream.Shared.Interfaces;

namespace ZoDream.PixelStudio.Plugins
{
    public class PluginCollection : Dictionary<string, List<PluginMenuItem>>, IPluginCollection
    {
        public void Add<T>(string group, string label)
        {
            group = group.ToUpper();
            var item = new PluginMenuItem(group, label, typeof(T));
            if (TryGetValue(group, out var items))
            {
                items.Add(item);
                return;
            }
            items =
            [
                item
            ];
            Add(group, items);
        }

        public PluginMenuItem[] Get(params string[] groups)
        {
            var items = new List<PluginMenuItem>();
            foreach (var group in groups)
            {
                if (TryGetValue(group.ToUpper(), out var res))
                {
                    if (items.Count > 0)
                    {
                        items.Add(null);
                    }
                    items.AddRange(res);
                }
            }
            return [.. items];
        }

        public string GetGroupName(string group)
        {
            group = group.ToUpper();
            return group switch
            {
                PluginMenuItem.ImportName => "导入",
                PluginMenuItem.PaintName => "绘制",
                PluginMenuItem.ExportName => "导出",
                _ => group,
            };
        }
    }
}
