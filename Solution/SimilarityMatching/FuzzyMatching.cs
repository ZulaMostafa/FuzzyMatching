using FuzztMatching.Core.FeatureMatrixCalculation;
using FuzztMatching.Core.MatrixOperations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FuzzyMatching.Algorithms
{
    public class FuzzyMatching
    {
        private float [][] PreprocessedMatrix;
        private List<string> sentenceDataset = new List<string>();
        public FuzzyMatching(int size, string path)
        {
            // do the preprocessing phase
            PreprocessedMatrix = CreateFeatureMatrix(size, path);
        }

        private float [] [] CreateFeatureMatrix(int size, string path)
        {
            // load data set
            loadDataset(size, path);
            // clean
            // create feature matrix
            return Preprocessor.Preprocessor.CreateFeatureMatrix(sentenceDataset);
        }

        private void loadDataset (int size,string path)
        {
            var reader = new StreamReader(File.OpenRead(@path));
            reader.ReadLine();
            for (int i=0; i<size;i++)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                sentenceDataset.Add(values[1]);
            }
        }
        public (string, float, int) MatchSentence(string sentence)
        {
            var ngramsLength = 3;
            // calculate ngrams for the sentence
            var inputSentenceNGrams = NGramsCalculator.GetSentenceNGramsAsync(sentence, ngramsLength);
            // calculate ngrams frequencies 
            var inputSentenceNGramFrequencies = FrequencyCalculator.GetNGramFrequencyAsync(inputSentenceNGrams);
            // match string, score, index

        }
    }
}
