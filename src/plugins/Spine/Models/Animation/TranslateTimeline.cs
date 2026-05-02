

using ZoDream.Shared.Numerics;

namespace ZoDream.Plugin.Spine.Models
{
    public class TranslateTimeline : CurveTimeline
    {
        public TranslateTimeline(int frameCount): base(frameCount)
        {
            Frames = new float[frameCount];
            Points = new Point[frameCount];
        }
        public int BoneIndex { get; set; }
        public float[] Frames { get; set; }

        public Point[] Points { get; set; }

        public override int PropertyId => ((int)TimelineType.X << 24) + BoneIndex;
    }
}
