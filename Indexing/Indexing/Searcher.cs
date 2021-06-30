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

        /// <summary>
        /// Проводит обработку запроса в обратной польской нотации. Допустимы операции "и" и "или"
        /// </summary>
        /// <param name="query">запрос, представляемый в виде очереди из термов и операций в обратной польской нотации</param>
        /// <returns></returns>
        public string Search(Queue<string> query)
        {
            var qstack = new Stack<List<(string, int, int)>>();
            while (query.Count > 0)
            {
                var term = query.Dequeue();               
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
            var stringResult = "";
            foreach(var element in qstack.Pop())
            {
                stringResult += $"#####path= {element.Item1}, line= {element.Item2}, word= {element.Item3} ";
            }
            return stringResult;
        }

        private List<(string, int, int)> Find(string term)
        {
            using (var fileStream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var buferredStream = new BufferedStream(fileStream))
            using (var streamReader = new StreamReader(buferredStream))
            {
                string line = "";
                var list = new List<(string, int, int)>();
                /////////////////////////////////////////////
                /////////////////////////////////////////////
                do
                {
                    line = streamReader.ReadLine();
                    if(line == "EOF")
                    {
                        var emptyList = new List<(string, int, int)>();
                        emptyList.Add(("Nothing found", -1, -1));
                        return emptyList;
                    }
                    //Console.WriteLine(line);
                }
                while (!line.StartsWith(term) && line.Split(' ')[0] != term);
                var termInfo = line.Split('+');

                for (var i = 1; i < termInfo.Count(); i++)
                {
                    var elements = termInfo[i].Split(',');
                    var path = elements[0].Trim(new char[] { '(', ' ' });
                    var stringNumber = Convert.ToInt32(elements[1]);
                    var wordNumber = Convert.ToInt32(elements[2].Trim(new char[] { ')', ' ' }));
                    list.Add((path, stringNumber, wordNumber));
                }
                return list;

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
