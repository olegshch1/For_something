﻿using System;
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
        static void Print(Dictionary<string, List<string>> result)
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

        static void Loop(Dictionary<string, List<string>> dict)
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
                s = Console.ReadLine();
            }
        }

        static void Main(string[] args)
        {
            var stemmer = new Stemmer();
            var files = Directory.GetFiles("..\\..\\Texts");
            foreach (var pathFile in files)
            {
                stemmer.StemFile(pathFile);
            }
            var result = stemmer.GetDict();
            Loop(result);
        }
    }
}
