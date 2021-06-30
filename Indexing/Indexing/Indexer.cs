using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PriorityQueues;

namespace Indexing
{
    /// <summary>
    /// класс, в котором содержимое из файла токенизируется, стеммится и индексируется по SPIMI
    /// </summary>
    public class Indexer
    {
        /// <summary>
        /// библиотечный стеммер
        /// </summary>
        private Iveonik.Stemmers.RussianStemmer stem = new Iveonik.Stemmers.RussianStemmer();

        /// <summary>
        /// здесь хранится путь к индексу
        /// </summary>
        private string indexPath;

        /// <summary>
        /// путь к папке текстовых словарей
        /// </summary>
        private string textedDictPath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "TextedDict";

        public Indexer() {}

        /// <summary>
        /// взятие результата
        /// </summary>
        /// <returns>терм-места</returns>
        public string GetIndexPath()
        {
            return indexPath;
        }

        /// <summary>
        /// обрабатывает текстовый файл на русском языке, создает текстовый локальный словарь
        /// </summary>
        /// <param name="path">путь файла</param>
        public void StemFile(string path, int docCounter)
        {
            var localdict = new Dictionary<string, List<(string, int, int)>>();
            Console.WriteLine($"stemming file in path== {path}");

            /// счет строк в файле
            var counterLine = 1;
            using (var reader = new StreamReader(path))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    ///счет номера слов в строке
                    var counterWord = 1;
                    foreach (var res in line.Split(new[] { ' ', ',', '.', '-', ':', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                    {                        
                        var ss = stem.Stem(res);
                        if (!localdict.ContainsKey(ss))
                        {
                            var local = new List<(string, int, int)>();
                            local.Add(($"path= {path}", counterLine, counterWord));
                            localdict.Add(ss, local);
                        }
                        else
                        {
                            List<(string, int, int)> value;
                            localdict.TryGetValue(ss, out value);
                            value.Add(($"path= {path}", counterLine, counterWord));
                        }
                        counterWord++;
                    }
                    counterLine++;
                    line = reader.ReadLine();
                }

                //сортировка внутри терма
                foreach (var postingList in localdict.Values)
                {
                    postingList.Sort();
                }

                var sortedDict = new SortedDictionary<string, List<(string, int, int)>>(localdict);
                Directory.CreateDirectory(textedDictPath);
                using (var streamWriter = File.CreateText(textedDictPath + Path.DirectorySeparatorChar + $"{docCounter}"))
                {
                    /////////////////////////////////////////////////////////////////////////////////
                    //Console.WriteLine($" ++++++++++++++++++++++++++++++++++++++++++++++++++ {docCounter}");
                    foreach (var element in sortedDict)
                    {
                        streamWriter.Write(element.Key);
                        ///////////////////////////////////////////////////////////////////////////
                        Console.WriteLine(element.Key);
                        foreach (var docId in element.Value)
                        {
                            streamWriter.Write($" {docId}");
                            ///////////////////////////////////////////////////////////////////////
                            Console.WriteLine($" {docId}");
                        }
                        streamWriter.WriteLine();
                        ////////////////////////////////////////////////////////////////////////////
                        //Console.WriteLine();
                    }
                }
            }
        }

        /// <summary>
        /// сливание локальных словарей
        /// </summary>
        public void Merge()
        {
            var dictBlocks = Directory.GetFiles(textedDictPath);
            for(int i = 0; i < dictBlocks.Length; i++)
            {
                Console.WriteLine($"merging block {dictBlocks[i]}");
            }
            var dictList = new ConcurrentBag<string>();
            var queue = new BinaryHeap<List<(string, int, int)>, string>(PriorityQueueType.Minimum);
            Parallel.ForEach(dictBlocks, block =>
            {
                var streamReader = new StreamReader(block);
                using (streamReader)
                {
                    var lines = File.ReadAllLines(block);
                    foreach (var line in lines)
                    {
                        dictList.Add(line);
                    }
                }
            });

            foreach (var termInfoString in dictList)
            {
                var term = termInfoString.Split(' ')[0];
                var termInfo = termInfoString.Split(' ');
                var postingList = new List<(string, int, int)>();

                for (var i = 1; i < termInfo.Count(); i++)
                {
                    var path = termInfo[i].Split(',', '(', ')')[0];
                    var stringNumber = Convert.ToInt32(termInfo[i].Split(',', '(', ')')[1]);
                    var wordNumber = Convert.ToInt32(termInfo[i].Split(',', '(', ')')[2]);
                    postingList.Add((path, stringNumber, wordNumber));
                }
                queue.Enqueue(postingList, term);
            }

            var finalPath = textedDictPath + Path.DirectorySeparatorChar + "FinalIndex";
            Directory.CreateDirectory(finalPath);
            indexPath = finalPath + Path.DirectorySeparatorChar + "FinalIndexText";
            using (var streamWriter = new StreamWriter(indexPath))
            {
                while (queue.Count != 0)
                {
                    var topPriority = queue.PeekPriority;
                    var headElement = queue.Dequeue();

                    while (queue.Count != 0 && topPriority == queue.PeekPriority)
                    {
                        var nextPostingList = queue.Dequeue();

                        foreach (var el in nextPostingList)
                        {
                            headElement.Add(el);
                        }
                    }

                    headElement.Sort();
                    streamWriter.Write($"{topPriority} ");
                    foreach (var element in headElement)
                    {
                        streamWriter.Write($" {element}");
                    }
                    streamWriter.WriteLine();
                }
            }
        }
    }
}
