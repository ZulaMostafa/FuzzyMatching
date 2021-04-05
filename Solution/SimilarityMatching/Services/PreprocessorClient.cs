
using FuzzyMatching.Definitions.Services;
using FuzzyMatching.FeatureMatrixCalculation;
using FuzzyMatching.MatrixOperations;
using System.Collections.Generic;
using System.Linq;

namespace FuzzyMatching.Preprocessor
{
    public static class PreprocessorClient : IPreprocessorClient
    {
        public static (float[][], float[], float[], string[]) CreateFeatureMatrix(List<string> dataset)
        {
            // calculate ngrams for each sentence
            var ngramsLength = 3;
            var inputSentenceDatasetNGrams = NGramsCalculator.GetSentenceNGramsBatchAsync(dataset, ngramsLength);//.GetAwaiter().GetResult();

            // calculate ngram frequencies
            var inputSentenceDatasetNGramFrequencies = FrequencyCalculator.GetNGramFrequencyBatchAsync(inputSentenceDatasetNGrams);//.GetAwaiter().GetResult();
            //var allSenteceList = inputSentenceDatasetNGrams.Append(inputSentenceNGrams).ToArray();
            var overallDataNgramFrequencies = FrequencyCalculator.GetOverallNGramFrequencyAsync(inputSentenceDatasetNGrams).GetAwaiter().GetResult();

            // get ngrams feature vector
            var allDataUniqueNGramsVector = overallDataNgramFrequencies.Keys.ToArray();

            // calculate TF

            var inputSentenceDatasetTFMatrix = TFCalculator.CalculateTFVectorBatchAsync(inputSentenceDatasetNGramFrequencies, allDataUniqueNGramsVector);//.GetAwaiter().GetResult();

            // calculate IDF
            int overallDataLength = dataset.Count + 1;
            var overallDataIDFVector = IDFCalculator.CalculateIDFVector(allDataUniqueNGramsVector, overallDataNgramFrequencies, overallDataLength);

            // calculate TF-IDF
            var inputSentenceDatasetTFIDFMatrix = CellOperations.MultiplyVectorCellsBatch(inputSentenceDatasetTFMatrix, overallDataIDFVector);

            // get scalar values
            var inputSentenceDataseetAbsoluteValues = DotProductCalculator.CalculateVectorAbsoluteValueBatch(inputSentenceDatasetTFIDFMatrix);

            return (inputSentenceDatasetTFIDFMatrix, inputSentenceDataseetAbsoluteValues, overallDataIDFVector, allDataUniqueNGramsVector);
        }
    }
}
