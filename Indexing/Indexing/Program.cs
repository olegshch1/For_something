using System;
using System.Collections.Generic;
using System.IO;

namespace Indexing
{
    class Program
    {   
        /// <summary>
        /// вывод на печать всего словаря
        /// </summary>
        /// <param name="result">итоговый словарь</param>
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
            Console.WriteLine("команда 'exit' для закрытия поисковика exit");
            Console.WriteLine("программа принимает запрос, состоящий из слов, операций И ('&'), ИЛИ ('|'), записанных в обратной польской нотации");

            var stem = new Iveonik.Stemmers.RussianStemmer();
            var s = Console.ReadLine();
            while (s != "exit")
            {
                var query = new Queue<string>();
                foreach (var res in s.Split(new[] { ' ', ',', '.', '-', ':', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (res == "&" || res == "|")
                        query.Enqueue(res);
                    else
                    {
                        query.Enqueue(stem.Stem(res));
                    }                           
                }
                var result = searcher.Search(query);
                Console.WriteLine($"found in {result.Item2} docs");
                Console.WriteLine(result.Item1);
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
            var srchr = new Searcher(indxr.GetIndexPath());
            Loop(srchr);
        }
    }
}
