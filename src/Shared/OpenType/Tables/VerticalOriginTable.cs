using ZoDream.Shared.Font;

namespace ZoDream.Shared.OpenType.Tables
{
    public class VerticalOriginTable : ITypefaceTable
    {
        public const string TableName = "VORG";

        public string Name => TableName;

        public short DefaultVertOriginY { get; internal set; }

        public VertOriginYMetrics[] VertOriginYMetrics { get; set; }
    }
}
