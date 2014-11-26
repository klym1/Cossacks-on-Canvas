using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace JSCompiler
{
    internal class Warning
    {
        public string type { get; set; }
        public string file { get; set; }
        public int lineno { get; set; }
        public int charno { get; set; }
        public string warning { get; set; }
        public string line { get; set; }
    }

    internal class Statistics
    {
        public int originalSize { get; set; }
        public int originalGzipSize { get; set; }
        public int compressedSize { get; set; }
        public int compressedGzipSize { get; set; }
        public int compileTime { get; set; }
    }

    class ClosureComplierResult
    {
        public string CompiledCode { get; set; }
        public Statistics Statistics { get; set; }
        public ICollection<Warning> Warnings { get; set; } 
    }
    
    class Program
    {
        static void Main()
        {
            var pairs = new Collection<KeyValuePair<string,string>>()
            {
                new KeyValuePair<string, string>("output_format", "json"),
                new KeyValuePair<string, string>("output_info", "compiled_code"),
                new KeyValuePair<string, string>("output_info", "warnings"),
                new KeyValuePair<string, string>("output_info", "statistics"),
                new KeyValuePair<string, string>("warning_level", "verbose"),
               new KeyValuePair<string, string>("compilation_level", "SIMPLE_OPTIMIZATIONS"),
               new KeyValuePair<string, string>("language", "ECMASCRIPT5")
            };

            //order matters
            var files = new[]
            {
                "sprites_png\\sprites-info.json",
                "js\\im.js",
                "js\\World.js",
                "js\\Command.js",
                "js\\Unit.js",
                "js\\UnitState.js",
                "js\\animationLoop.js",
                "js\\Render.js",
                "js\\Init.js"
            };
            
            foreach (var s in files)
            {
                pairs.Add(new KeyValuePair<string, string>("js_code", File.ReadAllText(Path.Combine("..\\..\\..\\..\\..", s))));
            }
            
            var content = new FormUrlEncodedContent(pairs);

            var httpClient = new HttpClient();

            var result = httpClient.PostAsync("http://closure-compiler.appspot.com/compile", content).Result;
            var resultContent = result.Content.ReadAsStringAsync().Result;

            var javascriptSerializer = new JavaScriptSerializer();

            var closureComplierResult = javascriptSerializer.Deserialize<ClosureComplierResult>(resultContent);

            File.WriteAllText("..\\..\\..\\..\\..\\compiledCode.js", closureComplierResult.CompiledCode);

            Console.WriteLine("Warnings: {0}", closureComplierResult.Warnings.Count);
            Console.ReadKey();
        }
    }
}
