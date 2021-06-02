using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// здесь хранится результат индексирования
        /// </summary>
        private Dictionary<string, List<(string, int, int)>> dict = new Dictionary<string, List<(string, int, int)>>();

        public Indexer() {}

        /// <summary>
        /// взятие результата
        /// </summary>
        /// <returns>терм-места</returns>
        public Dictionary<string, List<(string, int, int)>> GetDict()
        {
            return dict;
        }

        /// <summary>
        /// обрабатывает текстовый файл на русском языке, создает текстовый локальный словарь
        /// </summary>
        /// <param name="path"></param>
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
                using (var streamWriter = File.CreateText(".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "TextedDict" + $"{docCounter}"))
                {
                    /////////////////////////////////////////////////////////////////////////////////
                    //Console.WriteLine($" ++++++++++++++++++++++++++++++++++++++++++++++++++ {docCounter}");
                    foreach (var element in sortedDict)
                    {
                        streamWriter.Write(element.Key);
                        ///////////////////////////////////////////////////////////////////////////
                        //Console.WriteLine(element.Key);
                        foreach (var docId in element.Value)
                        {
                            streamWriter.Write($" {docId}");
                            ///////////////////////////////////////////////////////////////////////
                            //Console.Write($" {docId}");
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
            var dictBlocks = Directory.GetFiles(".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "TextedDict");
            var dictList = new ConcurrentBag<string>();
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
                var queue = new PriorityQueue<int>();

                for (var i = 1; i < postingList.Count; i++)
                {
                    var path = termInfo[i].Split(',', '(', ')')[0];
                    var stringNumber = Convert.ToInt32(termInfo[i].Split(',', '(', ')')[1]);
                    var wordNumber = Convert.ToInt32(termInfo[i].Split(',', '(', ')')[2]);
                    postingList.Add((path, stringNumber, wordNumber));
                }
                queue.Enqueue(term, postingList);
            }
        }
    }
}
