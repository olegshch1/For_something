using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iveonik.Stemmers;
using System.IO;

namespace Indexing
{
    class Program
    {   
        /// <summary>
        /// вывод на печать всего словаря
        /// </summary>
        /// <param name="result"></param>
        static void Print(Dictionary<string, List<(string, int, int)>> result)
        {
            foreach (var item in result)
            {
                Console.WriteLine(item.Key);
                foreach (var path in item.Value)
                {
                    Console.WriteLine(path);
                }
                Console.WriteLine(" ");
            }
        }

        /// <summary>
        /// петля поиска
        /// </summary>
        /// <param name="dict">то, где ищем</param>
        static void Loop(Dictionary<string, List<(string, int, int)>> dict)
        {
            Console.WriteLine("Search is running");
            Console.WriteLine("Write 'exit' for exit");
            Console.WriteLine("Write 'print dict' for seeing all dictionary");

            var stem = new Iveonik.Stemmers.RussianStemmer();
            var s = Console.ReadLine();
            while (s != "exit")
            {
                if (s == "print dict") Print(dict);
                else
                {
                    foreach (var res in s.Split(new[] { ' ', ',', '.', '-', ':', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        var ss = stem.Stem(res);
                        Console.WriteLine("'" + ss + "'");
                        if (dict.ContainsKey(ss))
                        {
                            var paths = dict[ss];
                            foreach (var path in paths)
                            {
                                Console.WriteLine(path);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Absolutely nothing");
                        }
                    }
                }
                Console.WriteLine("==================================");
                s = Console.ReadLine();
            }
        }

        /// <summary>
        /// запуск индексации и поисковика
        /// </summary>
        static void Main(string[] args)
        {
            var indxr = new Indexer();
            var files = Directory.GetFiles(".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "Texts");
            var docCounter = 1;
            foreach (var pathFile in files)
            {
                indxr.StemFile(pathFile, docCounter);
                docCounter++;
            }
            indxr.Merge();
            var result = indxr.GetDict();
            var srchr = new Searcher();
            Loop(result);
        }
    }
}
