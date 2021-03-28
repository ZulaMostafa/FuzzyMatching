using FuzzyMatching.Algorithms;
using System.Collections.Generic;
using System.IO;
using System;

namespace SimilarityMatching
{
    class Program
    {

        static void Main(string[] args)
        {
            // get test data
        /*    string sentence = "I want a transport for london";
            int[] sizes = new int[7] { 10, 100, 1000, 10000, 25000, 50000, 100000 };
            var reader = new StreamReader(File.OpenRead(@"C:\Users\v-kelhammady\OneDrive - Microsoft\Documents\GitHub\FuzzyMatching\largeDataset.csv"));
            Console.WriteLine("Dataset Loaded !!");
            List<string> sentenceDataset = new List<string>();
            //List<string> listB = new List<string>();
            int counter = 0;
            reader.ReadLine();

            //while(!reader.EndOfStream)
            foreach (var size in sizes)
            {
                while (counter <= size)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    sentenceDataset.Add(values[1]);
                    counter++;
                    // listB.Add(values[1]);
                }
                Console.WriteLine("sentence array extracted");
                //     var sentenceDataset = new List<string>
                // {
                //     "go out",
                //     "drink water",
                //     "do laundry",
                //     "play games",
                //     "blah blah blah"
                // };     

                // calculate similarity (no preprocessing)
                DateTime start = DateTime.Now;
                var result =  GetClosestSentence(sentence, sentenceDataset);
                DateTime end = DateTime.Now;
                TimeSpan ts = (end - start);
                Console.WriteLine("Elapsed Time for the program with size {0} is {1} s",size, ts.TotalSeconds);
                var closestMatch = result.Item1;
                Console.WriteLine(closestMatch);
                var index = result.Item2;
                Console.WriteLine(index);
                var score = result.Item3;
                Console.WriteLine(score);*/
            }
        }
    }

