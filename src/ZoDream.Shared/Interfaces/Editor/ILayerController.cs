using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.Interfaces
{
    public interface ILayerController
    {
        /// <summary>
        /// 只更新图层列表的选中
        /// </summary>
        public IImageLayer? SelectedItem { get; set; }
        /// <summary>
        /// 只更新图层列表的选中
        /// </summary>
        public IImageLayer[] SelectedItems { get; set; }

        public IImageLayerTree Items { get; }

        public void Add(IEnumerable<IImageLayer> items, IImageLayer? parent = null);

        public void Add(IImageLayer layer, IImageLayer? parent = null);
        public IImageLayer Add(IImageSource source);
        public IImageLayer Add(IImageSource source, string name);

        public void InsertAfter(IEnumerable<IImageLayer> items, IImageLayer layer);

        /// <summary>
        /// 移除图层并销毁
        /// </summary>
        /// <param name="layer"></param>
        public void Remove(IImageLayer layer);

        /// <summary>
        /// 清除全部图层
        /// </summary>
        public void Clear();
        public bool TryGet(Point point, [NotNullWhen(true)] out IImageLayer? layer);

        public IEnumerable<IImageLayer> Get(Rect rect);

        public void Paint(IImageStyleCanvas canvas);
    }
}
