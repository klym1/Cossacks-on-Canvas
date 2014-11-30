using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

        public IEnumerable<MotionData> FindUserMotionData()
        {
            //@MOTION_LB0 9 2 19 10 

            var search = _fileContents
                .Where(it => it.StartsWith("@"));

            foreach (var data in search)
            {
                var elems = data.Split(' ').Where(it => !string.IsNullOrWhiteSpace(it)).Select(it => it).ToArray();
                
                var totalDirections = Int32.Parse(elems[1]); //not used ( = 9 )
                
                var newMotionData = new MotionData();

                newMotionData.Name = elems[0];
                newMotionData.UserLc = Int32.Parse(elems[2]);

                newMotionData.Data =
                    elems.Skip(3)
                        .SelectEven()
                        .Zip(elems.Skip(3).SelectOdd(),
                            (s1, s2) => new Tuple<int, int>(Int32.Parse(s1), Int32.Parse(s2)))
                        .ToArray();
            }

            return new []{new MotionData()};
        }

        public IEnumerable<TransitiondataData> FindUserTransitionData()
        {
            //#TRANS01  9 10  6 4 6...

            var search = _fileContents
                .Where(it => it.StartsWith("#"));

            var transitionsCollection = new Collection<TransitiondataData>();

            foreach (var data in search)
            {
                var elems = data
                    .Split(' ')
                    .Where(it => !string.IsNullOrWhiteSpace(it))
                    .Select(it => it)
                    .ToArray();

                var totalDirections = Int32.Parse(elems[1]); //not used ( = 9 )
                var numberOfPairs = Int32.Parse(elems[2]);

                var newTransitionData = new TransitiondataData
                {
                    Name = elems[0],
                    Data = elems.Skip(3)
                        .SelectEven()
                        .Zip(elems.Skip(3).SelectOdd(),
                            (s1, s2) => new []{ Int32.Parse(s1), Int32.Parse(s2)})
                        .ToArray()
                };

                transitionsCollection.Add(newTransitionData);
            }

            return transitionsCollection;
        }
    }
}
