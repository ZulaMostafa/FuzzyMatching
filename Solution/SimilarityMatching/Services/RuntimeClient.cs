using FuzzyMatching.Core.FeatureMatrixCalculation;
using FuzzyMatching.Core.MatrixOperations;
using FuzzyMatching.Definitions.Models;
using FuzzyMatching.Definitions.Services;
using System.Collections.Generic;
using System.Linq;

namespace FuzzyMatching.Core.RunTime
{
    public class RuntimeClient : IRuntimeClient
    {
        public FuzzyMatchingResult MatchSentence(string sentence, ProcessedDataset preprocessedDataset, List<string> Dataset)
        {
            var result = new FuzzyMatchingResult();
            var ngramsLength = 3;
            // calculate ngrams for the sentence
            var inputSentenceNGrams = NGramsCalculator.GetSentenceNGramsAsync(sentence, ngramsLength);
            // calculate ngrams frequencies 
            var inputSentenceNGramFrequencies = FrequencyCalculator.GetNGramFrequencyAsync(inputSentenceNGrams);
            // calculate TF vector
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

            result.MatchingIndex = minIndex;
            result.MatchingScore = minValue;
            result.ClosestSentence = Dataset[minIndex];
            return result;
        }
    }
}
