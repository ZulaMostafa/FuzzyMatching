using FuzztMatching.FeatureMatrixCalculation;
using FuzztMatching.MatrixOperations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FuzzyMatching.Algorithms
{
    public class FuzzyMatching
    {
        
        private float [][] PreprocessedMatrix;
        private float [] ScalarValues;
        private string [] UniqueNgrams;
        private float[] IDFVector;
        private List<string> sentenceDataset = new List<string>();
        public FuzzyMatching(int size, string path)
        {
            // do the preprocessing phase
            (PreprocessedMatrix, ScalarValues,IDFVector, UniqueNgrams) = CreateFeatureMatrix(size, path);
        }

        private( float [] [],float [],float [],string []) CreateFeatureMatrix(int size, string path)
        {
            // load data set
            loadDataset(size, path);
            // clean
            // create feature matrix
            return Preprocessor.Preprocessor.CreateFeatureMatrix(sentenceDataset);
        }

        private void loadDataset (int size,string path)
        {
            var reader = new StreamReader(File.OpenRead(path));
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
           // var inputSentenceUniqueNGramsVector = inputSentenceNGramFrequencies.Keys.ToArray();
            // calculate TF vector
           // var inputSentenceTFVector = TFCalculator.CalculateTFVectorAsync(inputSentenceNGramFrequencies, inputSentenceUniqueNGramsVector);
            var inputSentenceTFVectorDataset = TFCalculator.CalculateTFVectorAsync(inputSentenceNGramFrequencies, UniqueNgrams);

            // calculate TF-IDF vector
         
            var inputSentenceTFIDFVectorDataset = CellOperations.MultiplyVectorCells(inputSentenceTFVectorDataset, IDFVector);

            // get absolute value
            var inputSentenceAbsoluteValue = DotProductCalculator.GetVectorAbsoluteValue(inputSentenceTFIDFVectorDataset);

            // calculate similarity

            var similarityValues = DotProductCalculator.CalculateDotProduct(inputSentenceTFIDFVectorDataset, inputSentenceAbsoluteValue, PreprocessedMatrix,ScalarValues);

            // match string, score, index
            // get most matching one
            float minValue = similarityValues.Min();
            int minIndex = similarityValues.ToList().IndexOf(minValue);

            // return
            return (sentenceDataset[minIndex],minValue, minIndex);
        }
    }
}
