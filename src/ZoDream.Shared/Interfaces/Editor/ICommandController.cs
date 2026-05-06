using System;

namespace ZoDream.Shared.Interfaces
{
    public interface ICommandController : IDisposable
    {

        public bool IsEnabled { get; }

        public void Initialize(IImageLayer[] items);

        public void Paint(IImageCanvas canvas);
    }
}
