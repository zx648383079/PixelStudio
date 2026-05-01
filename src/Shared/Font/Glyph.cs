using System;
using System.Collections.Generic;
using System.Numerics;
using ZoDream.Shared.Numerics;

namespace ZoDream.Shared.Font
{
    public class Glyph : ICloneable
    {
        public float AdvanceWidth { get; set; }

        public Rect BoundingBox { get; set; }

        public Vector2 SideBearings { get; set; }

        public IList<GlyphContour> Contours { get; set; } = [];

        public float Width => BoundingBox.Width;

        public float Height => BoundingBox.Height;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
