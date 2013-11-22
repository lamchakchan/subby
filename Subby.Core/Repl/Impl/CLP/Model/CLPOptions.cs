using System.Collections.Generic;
using System.Reflection;
using CommandLine;
using CommandLine.Text;
using Subby.Core.Repl.Model;

namespace Subby.Core.Repl.Impl.CLP.Model
{
    public class CLPOptions : Options
    {
        [Option('f', "format", Required = false,
            HelpText =
                "The format type of the source file(s).  Json, Xml, Newline delimited and etc.  Supported values are [json, xml, nl]."
            )]
        public override string SourceType
        {
            get { return base.SourceType; }
            set { base.SourceType = value; }
        }

        [Option('d', "dest", Required = true, HelpText = "The filepath for where the results are written to.")]
        public override string DestinationFilePath
        {
            get { return base.DestinationFilePath; }
            set { base.DestinationFilePath = value; }
        }

        [OptionList('s', "src", Required = true, Separator = ',',
            HelpText =
                "The filepaths for where the variable files are read from.  Use comma for a delimited list of file path(s)."
            )]
        public override List<string> SourceFilePaths
        {
            get { return base.SourceFilePaths; }
            set { base.SourceFilePaths = value; }
        }

        [Option('t', "tar", Required = true, HelpText = "The file path for the target tokenization file.")]
        public override string TargetFilePath 
        { 
            get { return base.TargetFilePath; } 
            set { base.TargetFilePath = value; }
        }

        [Option('h', "help", HelpText = "Help screen")]
        public override bool Help
        {
            get; set;
        }

        public string GetUsage()
        {
            var help = new HelpText
            {
                Heading = new HeadingInfo("Subby", Assembly.GetExecutingAssembly().GetName().Version.ToString()),
                Copyright = new CopyrightInfo("CodeLyfe LLC", 2013),
                AdditionalNewLineAfterOption = true,
                AddDashesToOption = true
            };
            help.AddPreOptionsLine(System.Environment.NewLine);
            help.AddPreOptionsLine(@"Usage1: subby -s file.json,c:\path\file.xml -t template.config -d final.config");
            help.AddPreOptionsLine(@"Usage2: subby -s ..\file.json -t template.config -d .\newFolder\final.config");
            help.AddPreOptionsLine(@"Usage3: subby -f json -s file1.ext -t template.config -d final.config");

            help.AddOptions(this);

            return help;
        }
    }
}
