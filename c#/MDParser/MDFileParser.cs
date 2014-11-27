using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MDParser
{
    public class MdFileParser
    {
        private string url;
        private readonly string[] _fileContents;

        public MdFileParser(string url)
        {
           this.url = url;
           this._fileContents = File.ReadAllLines(this.url);
        }

        public string [][] FindUserLCShadow()
        {
            //USERLC 4 greh  shadow -62 -91

            var search = _fileContents
                .Where(it => it.StartsWith("USERLC"))
                .Select(it => it.Split(' ')
                    .Where(o => !string.IsNullOrWhiteSpace(o))
                    .ToArray())
                .ToArray();
            
            return search;
        }
    }
}
