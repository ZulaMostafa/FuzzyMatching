using SimilarityMatching.Core.FeatureMatrixCalculation;
using SimilarityMatching.Core.MatrixOperations;
using System.Collections.Generic;
using System.Linq;

namespace SimilarityMatching.SimilarityMatchingAlgorithms
{
    public static class SimilarityMatchingNoPreprocessing
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
            var inputSentenceNGrams = NGramsCalculator.GetSentenceNGrams(sentence, ngramsLength);
            var inputSentenceDatasetNGrams = NGramsCalculator.GetSentenceNGramsBatch(sentenceDataset, ngramsLength);

            // calculate ngram frequencies
            var inputSentenceNGramFrequencies = FrequencyCalculator.GetNGramFrequency(inputSentenceNGrams);
            var inputSentenceDatasetNGramFrequencies = FrequencyCalculator.GetNGramFrequencyBatch(inputSentenceDatasetNGrams);
            var allSenteceList = inputSentenceDatasetNGrams.Append(inputSentenceNGrams).ToArray();
            var overallDataNgramFrequencies = FrequencyCalculator.GetOverallNGramFrequency(allSenteceList);

            // get ngrams feature vector
            var allDataUniqueNGramsVector = overallDataNgramFrequencies.Keys.ToArray();

            // calculate TF
            var inputSentenceTFVector = TFCalculator.CalculateTFVector(inputSentenceNGramFrequencies, allDataUniqueNGramsVector);
            var inputSentenceDatasetTFMatrix = TFCalculator.CalculateTFVectorBatch(inputSentenceDatasetNGramFrequencies, allDataUniqueNGramsVector);

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
