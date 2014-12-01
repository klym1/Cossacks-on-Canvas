using System.Collections.Generic;

namespace JSCompiler
{
    class ClosureComplierResult
    {
        public string CompiledCode { get; set; }
        public Statistics Statistics { get; set; }
        public ICollection<Warning> Warnings { get; set; } 
    }
}