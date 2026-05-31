using System.Threading.Tasks;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.PixelArt;

namespace ZoDream.Plugin.Case
{
    public class PixelUICreator : IPluginPainter
    {
        public Task PaintAsync(IImageBuilder builder)
        {
            var line = new LineCommand(builder)
            {
                From = new(10, 10),
                To = new(100, 100),
            };
            builder.Add(line);

            return Task.CompletedTask;
        }
    }
}
