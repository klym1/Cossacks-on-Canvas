using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using MDParser;

namespace ConvertAll
{
    internal class Program
    {
        private const string BaseDirectory = @"..\..\..\..\images\";
        private const string MDDirectory = @"..\..\..\..\MD\";
        private const string SpritesDirectory = @"..\..\..\..\sprites_png\";
        private static bool IsDebug { get; set; }

        private static void Main(string [] args)
        {
            var unit = "GRE";

            if (args.Length == 1)
            {
                if (args[0] == "--d")
                {
                    IsDebug = true;
                }
                else
                {
                    unit = args[0];
                }
            }

            var bitmapSaver = new BitmapSaver(SpritesDirectory);
            var bitmapProcessor = new BitmapProcessor(BaseDirectory);
            var mdParser = new MdFileParser(Path.Combine(MDDirectory, unit + ".md"));
            
            var userLCs = mdParser.FindUserLCShadow();
            var results = new ConcurrentStack<SpriteGrid>();

            Parallel.ForEach(userLCs, 
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = Environment.ProcessorCount
                }, userLCId =>
            {

                var sw = Stopwatch.StartNew();

                try
                {
                    var grid = bitmapProcessor.RunAllSteps(userLCId);

                    results.Push(grid); 
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("File not found: {0}", e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0}", e);
                }
                finally
                {
                    Console.WriteLine("{0} {1} - {2:g}", userLCId[1], userLCId[2].ToUpperInvariant(), sw.Elapsed);

                    sw.Stop();
                }
            });

            bitmapSaver.SaveJsonAndBitmaps(results);
        }
    }
}