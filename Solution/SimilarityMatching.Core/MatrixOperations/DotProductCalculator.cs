using System;
using System.Linq;

namespace SimilarityMatching.Core.MatrixOperations
{
    public static class DotProductCalculator
    {
        public static float[] CalculateDotProduct(float[] vector, float[][] matrix)
        {
            return matrix.Select(row => CalculateDotProduct(vector, row)).ToArray();
        }
        private static float CalculateDotProduct(float[] v1, float[] v2)
        {
            var v1Abs = GetVectorAbsoluteValue(v1);
            var v2Abs = GetVectorAbsoluteValue(v2);
            var multiplicationSum = GetMultiplicationSum(v1, v2);
            return (v1Abs * v2Abs) / multiplicationSum;
        }
        private static float GetMultiplicationSum(float[] v1, float[] v2)
        {
            float result = 0;
            for (var i = 0; i < v1.Length; i++)
            {
                result += v1[i] * v2[i];
            }
            return result;
        }

        private static float GetVectorAbsoluteValue(float[] v)
        {
            var sum = v.Select(value => value * value).Sum();
            return (float)Math.Sqrt(sum);
        }
    }
}
