using System;
using System.Diagnostics;

namespace MDParser
{
    [DebuggerDisplay("{Name} {UserLc}")]
    public struct MotionData
    {
        public string Name { get; set; }
        public int UserLc { get; set; }
        public Tuple<int,int> [] Data { get; set; }
    }

    [DebuggerDisplay("{Name} {UserLc}")]
    public struct TransitiondataData
    {
        public string Name { get; set; }
        public int [][] Data { get; set; }
    }
}