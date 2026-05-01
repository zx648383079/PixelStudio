using System.Collections.Generic;
using System.Numerics;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.Document
{
    public class SpriteUvLayer: SpriteLayer
    {
        /// <summary>
        /// 原图上的顶点 UV
        /// </summary>
        public IList<Vector2> VertexItems { get; set; } = [];
        /// <summary>
        /// 实际绘制的顶点
        /// </summary>
        public IList<Point> PointItems { get; set; } = [];
    }

}
