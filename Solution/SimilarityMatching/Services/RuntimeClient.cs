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
        public FuzzyMatchingResult MatchSentence(string sentence, ProcessedDataset processedDataset, List<string> Dataset, int ngramsLength = 3)
        {
            // calculate ngrams for the sentence
            var inputSentenceNGrams = NGramsCalculator.GetSentenceNGramsAsync(sentence, ngramsLength);
            
            // calculate ngrams frequencies 
            var inputSentenceNGramFrequencies = FrequencyCalculator.GetNGramFrequencyAsync(inputSentenceNGrams);
            
            // calculate TF vector
            var inputSentenceTFVectorDataset = TFCalculator.CalculateTFVectorAsync(inputSentenceNGramFrequencies, processedDataset.AllDataUniqueNGramsVector);
            
            // calculate TF-IDF vector
            var inputSentenceTFIDFVectorDataset = CellOperations.MultiplyVectorCells(inputSentenceTFVectorDataset, processedDataset.OverallDataIDFVector);
            
            // get absolute value
            var inputSentenceAbsoluteValue = DotProductCalculator.GetVectorAbsoluteValue(inputSentenceTFIDFVectorDataset);
            
            // calculate similarity
            var similarityValues = DotProductCalculator.CalculateDotProduct(inputSentenceTFIDFVectorDataset, inputSentenceAbsoluteValue, processedDataset.InputSentenceDatasetTFIDFMatrix, processedDataset.InputSentenceDataseetAbsoluteValues);

            // get most matching one (match string, score, index)
            float minValue = similarityValues.Min();
            int minIndex = similarityValues.ToList().IndexOf(minValue);

            // return
            return new FuzzyMatchingResult {
                MatchingIndex = minIndex,
                MatchingScore = minValue,
                ClosestSentence = Dataset[minIndex]
            };
        }
    }
}
