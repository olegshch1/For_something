using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indexing
{
    public class Searcher
    {
        private readonly string path;

        public Searcher(string path)
        {
            this.path = path;
        }

        public string Search(string query)
        {
            var terms = query.Split(' ');
            var qstack = new Stack<string>();
            foreach(var term in terms)
            {
                switch (term)
                {
                    case "&":
                        {
                            var t1 = qstack.Pop();
                            var t2 = qstack.Pop();
                            //qstack.Push(AndOp(t1, t2));
                            break;
                        }
                    case "|":
                        {
                            var t1 = qstack.Pop();
                            var t2 = qstack.Pop();
                            //qstack.Push(OrOp(t1, t2));
                            break;
                        }
                    default:
                        {
                            qstack.Push(Find(term));
                            break;
                        }
                }
            }

            
            return Find(qstack.Pop());
            
        }

        private string Find(string term)
        {
            using (var fileStream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var buferredStream = new BufferedStream(fileStream))
            using (var streamReader = new StreamReader(buferredStream))
            {
                string line = "";
                /////////////////////////////////////////////
                /////////////////////////////////////////////
                do
                {
                    line = streamReader.ReadLine();
                    Console.WriteLine(line);
                }
                while (!line.StartsWith(term) && line.Split(' ')[0] != term);
                return line;
            }
        }

        /*OrOp(string term1, string term2)
        {
            var splitInfo1 = Find(term1).Split(' ');
            var splitInfo2 = Find(term2).Split(' ');
            Find(term2);
            var ind1 = new List<(string, int, int)>();
            var ind2 = new List<(string, int, int)>();
            for(int i = 1; i < splitInfo1.Length; i = i + 3)
            {
                var path = splitInfo1[i];
                var lineNumber = Convert.ToInt32(splitInfo1[i + 1]);
                var wordNumber = Convert.ToInt32(splitInfo1[i + 2]);
                ind1.Add((path, lineNumber, wordNumber));
            }
            for (int i = 1; i < splitInfo2.Length; i = i + 3)
            {
                var path = splitInfo2[i];
                var lineNumber = Convert.ToInt32(splitInfo2[i + 1]);
                var wordNumber = Convert.ToInt32(splitInfo2[i + 2]);
                ind2.Add((path, lineNumber, wordNumber));
            }
            var ind3 = ind1;
            for(int i = 0; i < ind2.Count; i++)
            {
                ind3.Add(ind2[i]);
            }
            var res = ""
        }*/

        /*AndOp(string term1, string term2)
        {

        }*/
    }
}
