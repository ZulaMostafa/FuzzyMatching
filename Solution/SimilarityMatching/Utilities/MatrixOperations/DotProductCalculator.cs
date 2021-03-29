using System;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace FuzzyMatching.MatrixOperations
{
    public static class DotProductCalculator
    {
        public static float[] CalculateDotProduct(float[] vector, float[][] matrix)
        {
            return matrix.AsParallel().Select(row => CalculateDotProduct(vector, row)).ToArray();
        }
        public static float[] CalculateDotProduct(float[] vector, float vectorAbs, float[][] matrix, float [] matrixAbs)
        {
            return matrix.AsParallel().Select((row,index) => CalculateDotProduct(vector, row,vectorAbs,matrixAbs[index])).ToArray();
        }
       
        public static float [] CalculateVectorAbsoluteValueBatch (float [][] matrix)
        {
            return matrix.AsParallel().Select(row => GetVectorAbsoluteValue(row)).ToArray();
        }
        private static float CalculateDotProduct(float[] v1,  float[] v2)
        {
            var v1Abs = GetVectorAbsoluteValue(v1);
            var v2Abs = GetVectorAbsoluteValue(v2);
            var multiplicationSum = GetMultiplicationSum(v1, v2);
            return (v1Abs * v2Abs) / multiplicationSum;
        }
        private static float CalculateDotProduct(float[] v1, float[] v2, float v1Abs, float v2Abs)
        {
            var multiplicationSum = GetMultiplicationSum(v1, v2);
            return (v1Abs * v2Abs) / multiplicationSum;
        }
        private static float GetMultiplicationSum(float[] v1, float[] v2)
        {
            // math.net

            Vector<float> vA = Vector<float>.Build.Dense(v1);
            Vector<float> vB = Vector<float>.Build.Dense(v2);
            float result2 = vA.DotProduct( vB);
            return result2;
            
            // norml approach 
            //float result = 0;
            /*for (var i = 0; i < v1.Length; i++)
            {
                result += v1[i] * v2[i];
            }
            return result;*/
        }

        public static float GetVectorAbsoluteValue(float[] v)
        {
   
            var sum = v.Select(value => value * value).Sum();
            return (float)Math.Sqrt(sum);
        }
    }
}
