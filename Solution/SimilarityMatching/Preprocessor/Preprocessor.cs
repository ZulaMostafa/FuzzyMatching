
using System;
using System.Collections.Generic;
using System.Linq;

namespace FuzzyMatching.Preprocessor
{
    public static class Preprocessor
    {
        public static double[][] CreateFeatureMatrix(List<string> dataset)
        {
            // calculate ngrams for each sentence
            var ngramsLength = 3;
            //DateTime start = d;
            var inputSentenceDatasetNGrams = NGramsCalculator.GetSentenceNGramsBatchAsync(dataset, ngramsLength);//.GetAwaiter().GetResult();
            //DateTime end = d;
            //TimeSpan ts = (end - start);
            //Console.WriteLine("Elapsed Time for the Ngrms calc is {0} ms", ts.Milliseconds);

            // calculate ngram frequencies

            //start = d;
            var inputSentenceDatasetNGramFrequencies = FrequencyCalculator.GetNGramFrequencyBatchAsync(inputSentenceDatasetNGrams);//.GetAwaiter().GetResult();
            var allSenteceList = inputSentenceDatasetNGrams.Append(inputSentenceNGrams).ToArray();
            var overallDataNgramFrequencies = FrequencyCalculator.GetOverallNGramFrequencyAsync(allSenteceList);//.GetAwaiter().GetResult();
            //end = d;
            //ts = (end - start);
           // Console.WriteLine("Elapsed Time for the ngram frequencies is {0} ms", ts.Milliseconds);

            // get ngrams feature vector
            var allDataUniqueNGramsVector = overallDataNgramFrequencies.Keys.ToArray();

            // calculate TF
            var inputSentenceTFVector = TFCalculator.CalculateTFVectorAsync(inputSentenceNGramFrequencies, allDataUniqueNGramsVector);//.GetAwaiter().GetResult();
            //start = d;
            var inputSentenceDatasetTFMatrix = TFCalculator.CalculateTFVectorBatchAsync(inputSentenceDatasetNGramFrequencies, allDataUniqueNGramsVector);//.GetAwaiter().GetResult();
            //end = d;
            //ts = (end - start);
           // Console.WriteLine("Elapsed Time for the TF calc is {0} ms", ts.TotalMilliseconds);
            // calculate IDF
            int overallDataLength = dataset.Count + 1;
            //start = d;
            var overallDataIDFVector = IDFCalculator.CalculateIDFVector(allDataUniqueNGramsVector, overallDataNgramFrequencies, overallDataLength);
            //end = d;
            //ts = (end - start);
           // Console.WriteLine("Elapsed Time for the IDF calc is {0} ms", ts.TotalMilliseconds);

            // calculate TF-IDF
            var inputSentenceTFIDFVector = CellOperations.MultiplyVectorCells(inputSentenceTFVector, overallDataIDFVector);
            //start = d;
            var inputSentenceDatasetTFIDFMatrix = CellOperations.MultiplyVectorCellsBatch(inputSentenceDatasetTFMatrix, overallDataIDFVector);
            return null;
        }
    }
}
