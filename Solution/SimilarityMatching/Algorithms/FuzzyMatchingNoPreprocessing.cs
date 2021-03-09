using FuzztMatching.Core.FeatureMatrixCalculation;
using FuzztMatching.Core.MatrixOperations;
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
            var inputSentenceDatasetNGrams = NGramsCalculator.GetSentenceNGramsBatchAsync(sentenceDataset, ngramsLength).GetAwaiter().GetResult();

            // calculate ngram frequencies
            var inputSentenceNGramFrequencies = FrequencyCalculator.GetNGramFrequencyAsync(inputSentenceNGrams).GetAwaiter().GetResult();
            var inputSentenceDatasetNGramFrequencies = FrequencyCalculator.GetNGramFrequencyBatchAsync(inputSentenceDatasetNGrams).GetAwaiter().GetResult();
            var allSenteceList = inputSentenceDatasetNGrams.Append(inputSentenceNGrams).ToArray();
            var overallDataNgramFrequencies = FrequencyCalculator.GetOverallNGramFrequencyAsync(allSenteceList).GetAwaiter().GetResult();

            // get ngrams feature vector
            var allDataUniqueNGramsVector = overallDataNgramFrequencies.Keys.ToArray();

            // calculate TF
            var inputSentenceTFVector = TFCalculator.CalculateTFVectorAsync(inputSentenceNGramFrequencies, allDataUniqueNGramsVector).GetAwaiter().GetResult();
            var inputSentenceDatasetTFMatrix = TFCalculator.CalculateTFVectorBatchAsync(inputSentenceDatasetNGramFrequencies, allDataUniqueNGramsVector).GetAwaiter().GetResult();

            // calculate IDF
            int overallDataLength = sentenceDataset.Count + 1;
            var overallDataIDFVector = IDFCalculator.CalculateIDFVector(allDataUniqueNGramsVector, overallDataNgramFrequencies, overallDataLength);

            // calculate TF-IDF
            var inputSentenceTFIDFVector = CellOperations.MultiplyVectorCells(inputSentenceTFVector, overallDataIDFVector);
            var inputSentenceDatasetTFIDFMatrix = CellOperations.MultiplyVectorCellsBatch(inputSentenceDatasetTFMatrix, overallDataIDFVector);

            // get dot product
            var similarityValues = DotProductCalculator.CalculateDotProduct(inputSentenceTFIDFVector, inputSentenceDatasetTFIDFMatrix);

            // get most matching one
            float minValue = similarityValues.Min();
            int minIndex = similarityValues.ToList().IndexOf(minValue);

            // return
            return (sentenceDataset[minIndex], minIndex, minValue);
        }
    }
}
