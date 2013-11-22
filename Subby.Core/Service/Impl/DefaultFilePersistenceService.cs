using System.IO;
using Subby.Core.Model;

namespace Subby.Core.Service.Impl
{
    public class DefaultFilePersistenceService : IFileResultPersistenceService
    {
        public void Write(FileDestinationContext destination, string content)
        {
            var directoryPath = Path.GetDirectoryName(destination.FilePath);

            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (var writer = new StreamWriter(destination.FilePath, false))
            {
                writer.Write(content);
            }
        }
    }
}