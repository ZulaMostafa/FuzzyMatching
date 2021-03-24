
using System;
using FuzztMatching.Core;
using System.Collections.Generic;
using System.Linq;
using FuzztMatching.Core.FeatureMatrixCalculation;
using FuzztMatching.Core.MatrixOperations;

namespace FuzzyMatching.Preprocessor
{
    public static class Preprocessor
    {
        public static float[][] CreateFeatureMatrix(List<string> dataset)
        {
            // calculate ngrams for each sentence
            var ngramsLength = 3;
            var inputSentenceDatasetNGrams = NGramsCalculator.GetSentenceNGramsBatchAsync(dataset, ngramsLength);//.GetAwaiter().GetResult();
           
            // calculate ngram frequencies
            var inputSentenceDatasetNGramFrequencies = FrequencyCalculator.GetNGramFrequencyBatchAsync(inputSentenceDatasetNGrams);//.GetAwaiter().GetResult();
            //var allSenteceList = inputSentenceDatasetNGrams.Append(inputSentenceNGrams).ToArray();
            var overallDataNgramFrequencies = FrequencyCalculator.GetOverallNGramFrequencyAsync(inputSentenceDatasetNGrams);//.GetAwaiter().GetResult();
           
            // get ngrams feature vector
            var allDataUniqueNGramsVector = overallDataNgramFrequencies.Keys.ToArray();

            // calculate TF
   
            var inputSentenceDatasetTFMatrix = TFCalculator.CalculateTFVectorBatchAsync(inputSentenceDatasetNGramFrequencies, allDataUniqueNGramsVector);//.GetAwaiter().GetResult();

            // calculate IDF
            int overallDataLength = dataset.Count + 1;
            //start = d;
            var overallDataIDFVector = IDFCalculator.CalculateIDFVector(allDataUniqueNGramsVector, overallDataNgramFrequencies, overallDataLength);
           
            // calculate TF-IDF
            var inputSentenceDatasetTFIDFMatrix = CellOperations.MultiplyVectorCellsBatch(inputSentenceDatasetTFMatrix, overallDataIDFVector);

            return inputSentenceDatasetTFIDFMatrix;
        }
    }
}
