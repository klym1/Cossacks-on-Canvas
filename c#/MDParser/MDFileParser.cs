using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MDParser
{
    public class MDFileParser
    {
        private string url;
        private string[] fileContents;

//        private List<string> reverseStringFormat(string template, string str)
//        {
//            string pattern = "^" + Regex.Replace(template, @"\b(.)b\}", "(.*?)") + "$";
//
//            Regex r = new Regex(pattern);
//            Match m = r.Match(str);
//
//            List<string> ret = new List<string>();
//
//            for (int i = 1; i < m.Groups.Count; i++)
//            {
//                ret.Add(m.Groups[i].Value);
//            }
//
//            return ret;
//        }

        public MDFileParser(string url)
        {
            this.url = url;

            
           this.fileContents = File.ReadAllLines(this.url);
           
        }

        public string [][] FindUserLCShadow()
        {
            //USERLC 4 greh  shadow -62 -91

            var search = fileContents
                .Where(it => it.StartsWith("USERLC"))
                .Select(it => it.Split(' ').Where(o => !string.IsNullOrWhiteSpace(o)).ToArray()).ToArray();
            
            return search;
        }
    }
}
