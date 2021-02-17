using System.Collections.Generic;
using System.Linq;

namespace SimilarityMatching.Core.FeatureMatrixCalculation
{
    public static class FrequencyCalculator
    {
        public static Dictionary<string, int> GetNGramFrequency(string[] sentenceNGrams)
        {
            var result = new Dictionary<string, int>();
            foreach (var ngram in sentenceNGrams)
            {
                if (result.ContainsKey(ngram))
                {
                    result[ngram] += 1;
                }
                else
                {
                    result[ngram] = 1;
                }
            }
            return result;
        }

        public static List<Dictionary<string, int>> GetNGramFrequencyBatch(string[][] sentenceDatasetNGrams)
        {
            return sentenceDatasetNGrams.Select(sentenceNGrams => GetNGramFrequency(sentenceNGrams)).ToList();
        }

        public static Dictionary<string, int> GetOverallNGramFrequency(string[][] sentenceListNGrams)
        {
            var result = new Dictionary<string, int>();
            foreach (var sentenceNGrams in sentenceListNGrams)
            {
                foreach (var ngram in sentenceNGrams)
                {
                    if (result.ContainsKey(ngram))
                    {
                        result[ngram] += 1;
                    }
                    else
                    {
                        result[ngram] = 1;
                    }
                }
            }
            return result;
        }
    }
}
