using System.Collections.Generic;

namespace Subby.Core.Repl.Model
{
    public class Options
    {
        public virtual string SourceType { get; set; }
        public virtual List<string> SourceFilePaths { get; set; }
        public virtual string TargetFilePath { get; set; }
        public virtual string DestinationFilePath { get; set; }
        public virtual bool Help { get; set; }
        public virtual bool Print { get; set; }
    }
}
