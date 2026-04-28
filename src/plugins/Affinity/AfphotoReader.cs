using System;
using System.Threading.Tasks;
using ZoDream.Shared.Document;

namespace ZoDream.Plugin.Affinity
{
    internal class AfphotoReader : IProjectReader
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
