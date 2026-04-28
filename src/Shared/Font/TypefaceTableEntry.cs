namespace ZoDream.Shared.Font
{
    public class TypefaceTableEntry : ITypefaceTableEntry
    {
        public string Name { get; set; }
        public long Offset { get; set; }
        /// <summary>
        /// 校验和， 表的内容以 uint 形式读取相加，不足4位补 0
        /// </summary>
        public uint CheckSum { get; set; }
        public long Length { get; set; }
    }
}
