using System;
using System.Diagnostics.CodeAnalysis;

namespace ZoDream.Shared.Font
{
    public class Typeface : ITypeface
    {
        public ITypefaceTableCollection Items { get; } = new TypefaceTableCollection();
        public bool Contains(char character)
        {
            throw new NotImplementedException();
        }

        public void TryGet(char character, [NotNullWhen(true)] Glyph? result)
        {
            throw new NotImplementedException();
        }
    }
}
