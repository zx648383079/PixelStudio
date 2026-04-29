using SkiaSharp;
using System;
using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.ImageEditor
{
    public class ImageComputedVertexStyle(Guid layerId) : 
        ImageComputedStyle(layerId), IImageComputedVertexStyle
    {
        /// <summary>
        /// 原图上的顶点
        /// </summary>
        public SKPoint[] SourceItems { get; set; } = [];
        /// <summary>
        /// 实际绘制的顶点
        /// </summary>
        public SKPoint[] PointItems { get; set; } = [];
    }
}
