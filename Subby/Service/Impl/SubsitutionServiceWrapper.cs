using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Subby.Core.Factory;
using Subby.Core.Model;
using Subby.Core.Repl;
using Subby.Core.Service;

namespace Subby.Service.Impl
{
    public class SubsitutionServiceWrapper
    {
        private readonly IArgumentParser _argumentParser;
        private readonly IHelpPrinter _helpPrinter;
        private readonly IContentTypeService _contentTypeService;
        private readonly IFileVariableContextFactory _fileVariableContextFactory;
        private readonly IFileTargetContextFactory _fileTargetContextFactory;
        private readonly IFileDestinationContextFactory _fileDestinationContextFactory;
        private readonly ISubstitutionContextFactory _substitutionContextFactory;
        private readonly ISubstitutionService _substitutionService;
        private readonly IFileResultPersistenceService _fileResultPersistenceService;

        public SubsitutionServiceWrapper(IArgumentParser argumentParser, IHelpPrinter helpPrinter, IContentTypeService contentTypeService, IFileVariableContextFactory fileVariableContextFactory, IFileTargetContextFactory fileTargetContextFactory, IFileDestinationContextFactory fileDestinationContextFactory, ISubstitutionContextFactory substitutionContextFactory, ISubstitutionService substitutionService, IFileResultPersistenceService fileResultPersistenceService)
        {
            _argumentParser = argumentParser;
            _helpPrinter = helpPrinter;
            _contentTypeService = contentTypeService;
            _fileVariableContextFactory = fileVariableContextFactory;
            _fileTargetContextFactory = fileTargetContextFactory;
            _fileDestinationContextFactory = fileDestinationContextFactory;
            _substitutionContextFactory = substitutionContextFactory;
            _substitutionService = substitutionService;
            _fileResultPersistenceService = fileResultPersistenceService;
        }

        public void Process(string[] args)
        {
            var parseResult = _argumentParser.Parse(args);

            if (args.Any() && !parseResult.Help)
            {
                try
                {
                    var variableContext = _fileVariableContextFactory.Build(_contentTypeService.ProcessCode(parseResult.SourceType), parseResult.SourceFilePaths);
                    var targetContext = _fileTargetContextFactory.Build(parseResult.TargetFilePath);
                    var context = _substitutionContextFactory.Build(variableContext, targetContext);

                    _substitutionService.Process(context);

                    var fileDestinationContext = _fileDestinationContextFactory.Build(parseResult.DestinationFilePath);
                    _fileResultPersistenceService.Write(fileDestinationContext, context.Result);

                    if (parseResult.Print)
                    {
                        PrintContext(context);
                    }
                }
                catch (Exception ex)
                {
                    PrintError(ex.Message);
                }
            }
            else
            {
                PrintHelp();
            }
        }

        private void PrintHelp()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(_helpPrinter.PrintHelp());
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("");
            Console.WriteLine(@"ERROR!! {0}", message);
            Console.WriteLine("");
            PrintHelp();
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private void PrintContext(SubstitutionContext context)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Variables From Source File(s)");
            Console.WriteLine("---------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var item in context.Variables.OrderBy(p => p.Key))
            {
                Console.WriteLine("{0} => {1}", item.Key, item.Value);
            }
            Console.WriteLine(string.Empty);

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Target File");
            Console.WriteLine("---------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(context.Target);
            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Empty);

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Result");
            Console.WriteLine("---------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(context.Result);
            Console.WriteLine(string.Empty);

            Console.ForegroundColor = ConsoleColor.Gray;
            
        }
    }
}
