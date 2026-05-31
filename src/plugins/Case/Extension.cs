using ZoDream.Shared.Interfaces;

namespace ZoDream.Plugin.Case
{
    public static class Extension
    {
        public static void AddCase(this IPluginCollection service)
        {
            service.Add<PixelUICreator>("paint", "Pixel UI");
        }
    }
}
