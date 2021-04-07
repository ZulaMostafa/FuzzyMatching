
using FuzzyMatching.Definitions.Models;
using FuzzyMatching.Definitions.Services;
using FuzzyMatching.Core.FeatureMatrixCalculation;
using System.Collections.Generic;
using System.Linq;
using FuzzyMatching.Core.MatrixOperations;

namespace FuzzyMatching.Core.Preprocessor
{
    public  class PreprocessorClient : IPreprocessorClient
    {
      

         public  PreprocessedDataset  CreateFeatureMatrix(List<string> dataset)
        {
            PreprocessedDataset calculatedFeaturesMatrices = new PreprocessedDataset();
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

            calculatedFeaturesMatrices.InputSentenceDataseetAbsoluteValues = inputSentenceDataseetAbsoluteValues;
            calculatedFeaturesMatrices.InputSentenceDatasetTFIDFMatrix = inputSentenceDatasetTFIDFMatrix;
            calculatedFeaturesMatrices.OverallDataIDFVector = overallDataIDFVector;
            calculatedFeaturesMatrices.AllDataUniqueNGramsVector = allDataUniqueNGramsVector;

            return calculatedFeaturesMatrices;
        }
    }
}
