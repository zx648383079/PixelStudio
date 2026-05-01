using System.Collections.Generic;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.Document
{
    public class SpritePathLayer: SpriteLayer
    {
        /// <summary>
        /// 实际绘制的顶点
        /// </summary>
        public IList<Point> PointItems { get; set; } = [];

        public Color FillColor { get; set; }
        public Color StrokeColor { get; set; }

        public float StrokeWidth { get; set; }
    }

}
