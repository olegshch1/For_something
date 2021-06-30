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
                                qstack.Push(OrOp(t1, t2));
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

        List<(string, int, int)> OrOp(List<(string, int, int)> term1, List<(string, int, int)> term2)
        {
            var res = term1.Concat(term2).ToList();
            res.Sort();
            return res;
        }

        /*AndOp(string term1, string term2)
        {

        }*/
    }
}
