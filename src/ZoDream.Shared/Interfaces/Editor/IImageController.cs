namespace ZoDream.Shared.Interfaces
{
    public interface IImageController
    {
        public IImageStyler Styler { get; }
        /// <summary>
        /// 一个默认的
        /// </summary>
        public IImageStyler DefaultStyler { get; }
        /// <summary>
        /// 一个实时的
        /// </summary>
        public IImageStyler RealStyler { get; }
        public void Initialize(IImageShell shell);
    }
}
