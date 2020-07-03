using System.Collections.Generic;
using CommandLine;

namespace GoszakupParser
{
    public class CommandLineOptions
    {
        [Option('p', "parsers", Required = true, HelpText = "Determine parsers")]
        public IEnumerable<string> Parsers { get; set; }
        
        [Option('f', "force", Default = false, HelpText = "Ignores 'active' and 'parsed' fields")]
        public bool Force { get; set; }
        
        [Option('i', "ignore", Default = false, HelpText = "Ignores 'parsed' field")]
        public bool Ignore { get; set; }
    }
}