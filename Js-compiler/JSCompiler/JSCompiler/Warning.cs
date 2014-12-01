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
}