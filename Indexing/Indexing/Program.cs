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
        
        static void Main(string[] args)
        {
            var stemmer = new Stemmer();
            var files = Directory.GetFiles("..\\..\\Texts");
            foreach (var pathFile in files)
            {
                stemmer.StemFile(pathFile);
            }

            var result = stemmer.GetDict();
            foreach (var item in result)
            {
                Console.WriteLine(item.Key);
                foreach (var path in item.Value)
                {
                    Console.WriteLine(path);
                }
                Console.WriteLine(" ");
            }
            Console.ReadKey();
        }
    }
}
