namespace ZoDream.Shared.OpenType
{
    public class TTCFileHeader
    {
        public ushort MajorVersion;
        public ushort MinorVersion;
        public int[] OffsetTables;
        #region version 2
        /// <summary>
        /// DSIG
        /// </summary>
        public uint DsigTag;
        public uint DsigLength;
        public uint DsigOffset;
        #endregion

    }
}
