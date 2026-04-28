using System;
using System.IO;
using ZoDream.Shared.Font;
using ZoDream.Shared.IO;

namespace ZoDream.Shared.OpenType
{
    public class TTFReader(EndianReader reader) : ITypefaceReader
    {
        public TTFReader(Stream input) : this(new EndianReader(input, EndianType.BigEndian, false))
        {

        }

        public ITypefaceCollection Read()
        {
            // 0x00010000 or 0x4F54544F OTTO
            var majorVersion = reader.ReadUInt16();
            var minorVersion = reader.ReadUInt16();
            var res = new TypefaceCollection
            {
                ReadTypeface()
            };
            return res;
        }

        public ITypeface ReadTypeface()
        {
            var res = new Typeface();
            var tableCount = reader.ReadUInt16();
            var searchRange = reader.ReadUInt16(); // (2 ^ (int)Math.Floor(Math.Log2(tableCount))) * 16;
            var entrySelector = reader.ReadUInt16(); // Math.Log2(searchRange/16)
            var rangeShift = reader.ReadUInt16(); // tableCount * 16 - searchRange
            var entries = new TypefaceTableEntry[tableCount];
            for (int i = 0; i < tableCount; i++)
            {
                entries[i] = ReadTableEntry();
            }
            var data = entries.ToCollection();
            var serializer = new TypefaceTableSerializer(reader.BaseStream, new TypefaceSerializer(OTFReader.Converters), data);
            foreach (var item in entries)
            {
                serializer.TryGet(item, out _);
            }
            return res;
        }

        private TypefaceTableEntry ReadTableEntry()
        {
            return new TypefaceTableEntry()
            {
                Name = reader.ReadString(4),
                CheckSum = reader.ReadUInt32(),
                Offset = reader.ReadUInt32(),
                Length = reader.ReadUInt32(),
            };
        }

        public void Dispose()
        {
            reader.Dispose();
        }
    }
}
