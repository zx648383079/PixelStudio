using System;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.ImageEditor
{
    public class ImageComputedVertexStyle(Guid layerId) : 
        ImageComputedStyle(layerId), IImageComputedVertexStyle
    {
        /// <summary>
        /// 原图上的顶点
        /// </summary>
        public Point[] SourceItems { get; set; } = [];
        /// <summary>
        /// 实际绘制的顶点
        /// </summary>
        public Point[] PointItems { get; set; } = [];
    }
}
