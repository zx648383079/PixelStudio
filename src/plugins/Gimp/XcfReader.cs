using System;
using System.Threading.Tasks;
using ZoDream.Shared.Document;

namespace ZoDream.Plugin.Gimp
{
    public class XcfReader : IProjectReader
    {
        public Task<ProjectDocument?> ReadAsync(string fileName)
        {
            throw new NotImplementedException();
        }


        public Task WriteAsync(string fileName, ProjectDocument data)
        {
            throw new NotImplementedException();
        }

    }
}
