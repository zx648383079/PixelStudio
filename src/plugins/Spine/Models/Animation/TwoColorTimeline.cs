

using ZoDream.Shared.Numerics;

namespace ZoDream.Plugin.Spine.Models
{
    public class TwoColorTimeline : CurveTimeline
    {
        public TwoColorTimeline(int frameCount): base(frameCount)
        {
            Frames = new float[frameCount];
            ColorFrames = new Color[frameCount];
            Color2Frames = new Color[frameCount];
        }
        public int SlotIndex { get; set; }
        public float[] Frames { get; set; }// time, r, g, b, a, r2, g2, b2, ...

        public Color[] ColorFrames { get; set; }
        public Color[] Color2Frames { get; set; }

        public override int PropertyId => ((int)TimelineType.RGB2 << 24) + SlotIndex;
    }

    internal class RGB2Timeline : CurveTimeline
    {
        public RGB2Timeline(int frameCount) : base(frameCount)
        {
            Frames = new float[frameCount];
            ColorFrames = new Color[frameCount];
            Color2Frames = new Color[frameCount];
        }
        public int SlotIndex { get; set; }
        public float[] Frames { get; set; }// time, r, g, b, a, r2, g2, b2, ...

        public Color[] ColorFrames { get; set; }
        public Color[] Color2Frames { get; set; }

        public override int PropertyId => ((int)TimelineType.RGB2 << 24) + SlotIndex;
    }
}
