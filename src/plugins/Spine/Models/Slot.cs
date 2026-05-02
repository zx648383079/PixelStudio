using System.Text.Json.Serialization;
using ZoDream.Shared.Interfaces;
using ZoDream.Shared.Numerics;

namespace ZoDream.Plugin.Spine.Models
{
    public class Slot : ISkeletonSlot
    {
        public string Attachment { get; set; } = string.Empty;

        public string Bone { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int Index { get; set; }
        public Color? Color { get; set; }
        public Color? DarkColor { get; set; }
        [JsonPropertyName("blend")]
        public BlendMode BlendMode { get; set; }

        [JsonIgnore]
        public SlotRuntime Runtime { get; internal set; }

    }
}
