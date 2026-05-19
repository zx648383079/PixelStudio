using System;
using ZoDream.Shared.Font;
using ZoDream.Shared.IO;
using ZoDream.Shared.OpenType.Tables;

namespace ZoDream.Shared.OpenType.Converters
{
    public class MaxProfileConverter : TypefaceConverter<MaxProfileTable>
    {
        public override MaxProfileTable? Read(EndianReader reader, Type objectType, ITypefaceSerializer serializer)
        {
            var res = new MaxProfileTable
            {
                Version = reader.ReadUInt32(), // 0x00010000 == 1.0
                GlyphCount = reader.ReadUInt16(),
                MaxPointsPerGlyph = reader.ReadUInt16(),
                MaxContoursPerGlyph = reader.ReadUInt16(),
                MaxPointsPerCompositeGlyph = reader.ReadUInt16(),
                MaxContoursPerCompositeGlyph = reader.ReadUInt16(),
                MaxZones = reader.ReadUInt16(),
                MaxTwilightPoints = reader.ReadUInt16(),
                MaxStorage = reader.ReadUInt16(),
                MaxFunctionDefs = reader.ReadUInt16(),
                MaxInstructionDefs = reader.ReadUInt16(),
                MaxStackElements = reader.ReadUInt16(),
                MaxSizeOfInstructions = reader.ReadUInt16(),
                MaxComponentElements = reader.ReadUInt16(),
                MaxComponentDepth = reader.ReadUInt16()
            };
            return res;
        }

        public override void Write(EndianWriter writer, MaxProfileTable data, Type objectType, ITypefaceSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
