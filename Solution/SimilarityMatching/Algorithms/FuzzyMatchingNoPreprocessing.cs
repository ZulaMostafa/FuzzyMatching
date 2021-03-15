﻿using FuzztMatching.Core.FeatureMatrixCalculation;
using FuzztMatching.Core.MatrixOperations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FuzzyMatching.Algorithms
{
    public static class FuzzyMatchingNoPreprocessing
    {
        /// <summary>
        /// finds the closest match for a given "sentence" in some "sentenceDataset"
        /// example:
        ///     sentence = "play some games"
        ///     sentenceDataset = ["play games", "some other sentence", ...]
        ///     result: index = 0 -> "play games"
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="sentenceDataset"></param>
        public static (string, int, float) GetClosestSentence(string sentence, List<string> sentenceDataset)
        {
            // calculate ngrams for each sentence
            var ngramsLength = 3;
            var inputSentenceNGrams = NGramsCalculator.GetSentenceNGramsAsync(sentence, ngramsLength).GetAwaiter().GetResult();
            DateTime start = DateTime.Now;
            var inputSentenceDatasetNGrams = NGramsCalculator.GetSentenceNGramsBatchAsync(sentenceDataset, ngramsLength).GetAwaiter().GetResult();
            DateTime end = DateTime.Now;
            TimeSpan ts = (end - start);
            Console.WriteLine("Elapsed Time for the Ngrms calc is {0} ms", ts.Milliseconds);

            // calculate ngram frequencies
            var inputSentenceNGramFrequencies = FrequencyCalculator.GetNGramFrequencyAsync(inputSentenceNGrams).GetAwaiter().GetResult();

            start = DateTime.Now;
            var inputSentenceDatasetNGramFrequencies = FrequencyCalculator.GetNGramFrequencyBatchAsync(inputSentenceDatasetNGrams).GetAwaiter().GetResult();
            var allSenteceList = inputSentenceDatasetNGrams.Append(inputSentenceNGrams).ToArray();
            var overallDataNgramFrequencies = FrequencyCalculator.GetOverallNGramFrequencyAsync(allSenteceList).GetAwaiter().GetResult();
            end = DateTime.Now;
            ts = (end - start);
            Console.WriteLine("Elapsed Time for the ngram frequencies is {0} ms", ts.Milliseconds);

            // get ngrams feature vector
            var allDataUniqueNGramsVector = overallDataNgramFrequencies.Keys.ToArray();

            // calculate TF
            var inputSentenceTFVector = TFCalculator.CalculateTFVectorAsync(inputSentenceNGramFrequencies, allDataUniqueNGramsVector).GetAwaiter().GetResult();
            start = DateTime.Now;
            var inputSentenceDatasetTFMatrix = TFCalculator.CalculateTFVectorBatchAsync(inputSentenceDatasetNGramFrequencies, allDataUniqueNGramsVector).GetAwaiter().GetResult();
            end = DateTime.Now;
            ts = (end - start);
            Console.WriteLine("Elapsed Time for the TF calc is {0} ms", ts.TotalMilliseconds);
            // calculate IDF
            int overallDataLength = sentenceDataset.Count + 1;
            start = DateTime.Now;
            var overallDataIDFVector = IDFCalculator.CalculateIDFVector(allDataUniqueNGramsVector, overallDataNgramFrequencies, overallDataLength);
            end = DateTime.Now;
            ts = (end - start);
            Console.WriteLine("Elapsed Time for the IDF calc is {0} ms", ts.TotalMilliseconds);

            // calculate TF-IDF
            var inputSentenceTFIDFVector = CellOperations.MultiplyVectorCells(inputSentenceTFVector, overallDataIDFVector);
            start = DateTime.Now;
            var inputSentenceDatasetTFIDFMatrix = CellOperations.MultiplyVectorCellsBatch(inputSentenceDatasetTFMatrix, overallDataIDFVector);
            end = DateTime.Now;
            ts = (end - start);
            Console.WriteLine("Elapsed Time for the TF-IDF calc is {0} ms", ts.TotalMilliseconds);

            // get dot product
            start = DateTime.Now;
            var similarityValues = DotProductCalculator.CalculateDotProduct(inputSentenceTFIDFVector, inputSentenceDatasetTFIDFMatrix);
            end = DateTime.Now;
            ts = (end - start);
            Console.WriteLine("Elapsed Time for the dot product is {0} ms", ts.TotalMilliseconds);

            // get most matching one
            float minValue = similarityValues.Min();
            int minIndex = similarityValues.ToList().IndexOf(minValue);

            // return
            return (sentenceDataset[minIndex], minIndex, minValue);
        }
    }
}
