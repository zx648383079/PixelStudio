using ZoDream.Shared.Interfaces;

namespace ZoDream.Shared.Document
{
    public class ImageSection(string fileName, IImagePixel source, params string[] metaItems)
    {
        public string FileName { get; private set; } = fileName;

        public IImagePixel Source { get; private set; } = source;

        public string[] MetaItems { get; set; } = metaItems;
    }
}
