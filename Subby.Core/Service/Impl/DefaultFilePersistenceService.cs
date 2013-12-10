using System;
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

            try
            {
                using (var writer = new StreamWriter(destination.FilePath, false))
                {
                    writer.Write(content);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error writing to file ({0})", destination.FilePath), ex);
            }
        }
    }
}