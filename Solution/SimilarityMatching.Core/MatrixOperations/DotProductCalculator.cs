using System;
using System.Linq;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace FuzztMatching.Core.MatrixOperations
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

        private static float GetVectorAbsoluteValue(float[] v)
        {
            /* Vector<float> vA = Vector<float>.Build.Dense(v);
             vA.PointwiseMultiply(vA);
             var sum =vA.Sum();
             Console.WriteLine(sum);*/
           
            var sum = v.Select(value => value * value).Sum();
            //Console.WriteLine(sum2);
            return (float)Math.Sqrt(sum);
        }
    }
}
