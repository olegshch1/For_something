using System;
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
        /// обрабатывает текстовый файл на русском языке
        /// </summary>
        /// <param name="path"></param>
        public void StemFile(string path)
        {
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
                        if (!dict.ContainsKey(ss))
                        {
                            var local = new List<(string, int, int)>();
                            local.Add(($"path= {path}", counterLine, counterWord));
                            dict.Add(ss, local);
                        }
                        else
                        {
                            List<(string, int, int)> value;
                            dict.TryGetValue(ss, out value);
                            value.Add(($"path= {path}", counterLine, counterWord));
                        }
                        counterWord++;
                    }
                    counterLine++;
                    line = reader.ReadLine();
                }

                foreach (var postingList in dict.Values)
                {
                    postingList.Sort();
                }

                var sortedDict = new SortedDictionary<string, List<(string, int, int)>>(dict);

                using (var streamWriter = File.CreateText(".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "TextedDict"))
                {
                    foreach (var element in sortedDict)
                    {
                        streamWriter.Write(element.Key);
                        foreach (var docId in element.Value)
                        {
                            streamWriter.Write($" {docId}");
                        }
                        streamWriter.WriteLine();
                    }
                }
            }
        }
    }
}
