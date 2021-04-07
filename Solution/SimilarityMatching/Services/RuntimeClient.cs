using FuzzyMatching.Definitions.Models;
using FuzzyMatching.Definitions.Services;
using FuzzyMatching.Core.FeatureMatrixCalculation;
using System.Collections.Generic;
using System.Linq;
using FuzzyMatching.Core.MatrixOperations;

namespace FuzzyMatching.Core.RunTime
{
    public  class RuntimeClient : IRuntimeClient
    {
       
        

        public FuzzyMatchingResult MatchSentence(string sentence, PreprocessedDataset preprocessedDataset , List<string> Dataset)
        {
            FuzzyMatchingResult Results = new FuzzyMatchingResult();
            var ngramsLength = 3;
            // calculate ngrams for the sentence
            var inputSentenceNGrams = NGramsCalculator.GetSentenceNGramsAsync(sentence, ngramsLength);
            // calculate ngrams frequencies 
            var inputSentenceNGramFrequencies = FrequencyCalculator.GetNGramFrequencyAsync(inputSentenceNGrams);
            // var inputSentenceUniqueNGramsVector = inputSentenceNGramFrequencies.Keys.ToArray();
            // calculate TF vector
            // var inputSentenceTFVector = TFCalculator.CalculateTFVectorAsync(inputSentenceNGramFrequencies, inputSentenceUniqueNGramsVector);
            var inputSentenceTFVectorDataset = TFCalculator.CalculateTFVectorAsync(inputSentenceNGramFrequencies, preprocessedDataset.AllDataUniqueNGramsVector);

            // calculate TF-IDF vector

            var inputSentenceTFIDFVectorDataset = CellOperations.MultiplyVectorCells(inputSentenceTFVectorDataset, preprocessedDataset.OverallDataIDFVector);

            // get absolute value
            var inputSentenceAbsoluteValue = DotProductCalculator.GetVectorAbsoluteValue(inputSentenceTFIDFVectorDataset);

            // calculate similarity

            var similarityValues = DotProductCalculator.CalculateDotProduct(inputSentenceTFIDFVectorDataset, inputSentenceAbsoluteValue, preprocessedDataset.InputSentenceDatasetTFIDFMatrix, preprocessedDataset.InputSentenceDataseetAbsoluteValues);

            // match string, score, index
            // get most matching one
            float minValue = similarityValues.Min();
            int minIndex = similarityValues.ToList().IndexOf(minValue);

            Results.MatchingIndex = minIndex;
            Results.MatchingScore = minValue;
            Results.ClosestSentence = Dataset[minIndex];
            return Results;
        }

       
    }
}
