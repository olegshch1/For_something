using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indexing
{
    class Stemmer
    {
        private Iveonik.Stemmers.RussianStemmer stem = new Iveonik.Stemmers.RussianStemmer();
        
        private Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

        private int counterLine;

        public Stemmer() { }

        public Dictionary<string, List<string>> GetDict()
        {
            return dict;
        }

        public void StemFile(string path)
        {
            counterLine = 1;
            using (var reader = new StreamReader(path))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    var counterWord = 1;
                    foreach (var res in line.Split(new[] { ' ', ',', '.', '-', ':', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                    {                        
                        var ss = stem.Stem(res);
                        if (!dict.ContainsKey(ss))
                        {
                            var local = new List<string>();
                            local.Add($"path= {path}, string= {counterLine.ToString()}, word= {counterWord.ToString()}");
                            dict.Add(ss, local);
                        }
                        else
                        {
                            List<string> value;
                            dict.TryGetValue(ss, out value);
                            value.Add($"path= {path}, string= {counterLine.ToString()}, word= {counterWord.ToString()}");
                        }
                        counterWord++;
                    }
                    counterLine++;
                    line = reader.ReadLine();
                }
            }
        }
    }
}
