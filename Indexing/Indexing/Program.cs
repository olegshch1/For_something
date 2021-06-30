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
        static void Print(string path)
        {
            var line = File.ReadAllLines(path);
            foreach (var l in line)
            {
                Console.WriteLine(l);
            }
        }

        /// <summary>
        /// петля поиска
        /// </summary>
        /// <param name="dict">то, где ищем</param>
        static void Loop(Searcher searcher)
        {
            Console.WriteLine("Search is running");
            Console.WriteLine("Write 'exit' for exit");

            var stem = new Iveonik.Stemmers.RussianStemmer();
            var s = Console.ReadLine();
            while (s != "exit")
            {
                var query = "";
                foreach (var res in s.Split(new[] { ' ', ',', '.', '-', ':', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (res == "&" || res == "|")
                        query = query + res + " ";
                    else
                    {
                        var ss = stem.Stem(res);
                        query = query + ss + " ";
                    }                           
                }
                searcher.Search(query);
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
            var textsPath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "Texts";
            var files = Directory.GetFiles(textsPath);
            var docCounter = 1;
            foreach (var pathFile in files)
            {
                indxr.StemFile(pathFile, docCounter);
                docCounter++;
            }
            indxr.Merge();
            //Print(indxr.GetIndexPath());
            var srchr = new Searcher(indxr.GetIndexPath());
            Loop(srchr);
        }
    }
}
