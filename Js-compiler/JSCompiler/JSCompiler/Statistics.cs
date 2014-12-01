namespace JSCompiler
{
    internal class Statistics
    {
        public int originalSize { get; set; }
        public int originalGzipSize { get; set; }
        public int compressedSize { get; set; }
        public int compressedGzipSize { get; set; }
        public int compileTime { get; set; }
    }
}