using System.IO;
using ZoDream.Shared.Font;
using ZoDream.Shared.IO;
using ZoDream.Shared.OpenType.Converters;

namespace ZoDream.Shared.OpenType
{
    public class OTFReader(EndianReader reader) : TTFReader(reader)
    {

        public static TypefaceConverterCollection Converters = [
            new AxisVariationsConverter(),
            new BaseConverter(),
            new BitmapSizeConverter(),
            new CharacterGlyphMappingConverter(),
            new ColorConverter(),
            new ColorBitmapDataConverter(),
            new ColorBitmapLocationConverter(),
            new ColorPaletteConverter(),
            new CompactFontFormatConverter(),
            new CompactFontFormat2Converter(),
            new ControlValueConverter(),
            new ControlValueProgramConverter(),
            new CoverageConverter(),
            new CVTVariationsConverter(),
            new DigitalSignatureConverter(),
            new EmbeddedBitmapDataConverter(),
            new EmbeddedBitmapLocationConverter(),
            new EmbeddedBitmapScalingConverter(),
            new FontProgramConverter(),
            new FontVariationsConverter(),
            new GlyphDataConverter(),
            new GlyphDefinitionConverter(),
            new GlyphLocationsConverter(),
            new GlyphPositioningConverter(),
            new GlyphSubstitutionConverter(),
            new GlyphVariationsConverter(),
            new GridFittingScanConversionProcedureConverter(),
            new HeadConverter(),
            new HorizontalDeviceMetricsConverter(),
            new HorizontalHeaderConverter(),
            new HorizontalMetricsConverter(),
            new HorizontalMetricsVariationsConverter(),
            new IndexSubConverter(),
            new JustificationConverter(),
            new KernConverter(),
            new LinearThresholdConverter(),
            new MathConverter(),
            new MaxProfileConverter(),
            new MergeConverter(),
            new MetaConverter(),
            new MetricsConverter(),
            new MetricsVariationsConverter(),
            new NameConverter(),
            new OS2Converter(),
            new Pcl5Converter(),
            new PostConverter(),
            new StandardBitmapGraphicsConverter(),
            new StyleAttributesConverter(),
            new SvgConverter(),
            new VerticalDeviceMetricsConverter(),
            new VerticalHeaderConverter(),
            new VerticalMetricsConverter(),
            new VerticalMetricsVariationsConverter(),
            new VerticalOriginConverter(),
        ];

        public OTFReader(Stream input) : this(new EndianReader(input, EndianType.BigEndian, false))
        {
            // opentype 必须的表 cmap	head hhea hmtx maxp name OS/2 post
            // TrueType轮廓相关表格 cvt  fpgm glyf loca prep gasp
            // CFF轮廓相关表格 CFF  CFF2 VORG
            // SVG相关表 SVG 
            // 位图字形相关表 EBDT EBLC EBSC CBDT CBLC sbix
            // 高级排版属性表 BASE	 GDEF GPOS GSUB JSTF MATH
            // OpenType可变字体表 avar cvar fvar gvar HVAR MVAR STAT VVAR
            // 彩色字体相关表 COLR CPAL CBDT CBLC sbix SVG
        }
    }
}
