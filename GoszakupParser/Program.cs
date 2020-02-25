using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace GoszakupParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "Goszakup Parser";
            var str = File.ReadAllText("Configuration.json");
            var conf = JsonSerializer.Deserialize<Configuration>(str);
        }
    }
}