// using System;
using System.Linq;
//using System.Numerics;
// using System.Diagnostics;
// using NumSharp;
// using PandasNet.Impl;
// using PandasNet;
using MathNet.Numerics.LinearAlgebra;
// using MathNet.Numerics.LinearAlgebra.Double;

namespace FuzztMatching.Core.MatrixOperations
{
    
    public static class CellOperations
    {
        private static int counter =1;
        public static float[] MultiplyVectorCells(float[] vectorA, float[] vectorB)
        {
            //double[] vA = Array.ConvertAll(vectorA, x => (double)x);
           // double[] vB = Array.ConvertAll(vectorB, x => (double)x);


            // using NumSharp
            
        //     var arr1 = np.array(vectorA);
        //     var arr2 = np.array(vectorB);
        //    // arr1.reshape(vectorA.Length, 1);
        //     //arr2.reshape(vectorA.Length, 1);
        //     DateTime start = DateTime.Now;
        //     var arr3 = np.multiply(arr1, arr2);
        //     //var arr3 = arr1 * arr2;
        //     DateTime end = DateTime.Now;
        //     Console.WriteLine(counter);
        //     //Console.WriteLine(arr3.ToString());
        //     TimeSpan ts = (end - start);
        //     Console.WriteLine("Elapsed Time for numpy is {0} ms", ts.TotalMilliseconds);
        //     Console.WriteLine("----------------");

            // Vectors approach
            // var offset = Vector<float>.Count;
            // float[] result = new float[vectorA.Length];
            // int j;
            // DateTime start = DateTime.Now;
            // for (j = 0; j < vectorA.Length; j += offset)
            // {
            //     if (vectorA.Length - j < offset)
            //         break;
            //     var v1 = new Vector<float>(vectorA, j);
            //     var v2 = new Vector<float>(vectorB, j);
            //     (v1 * v2).CopyTo(result, j);
            // }

            // //remaining items
            // for (; j < vectorA.Length; ++j)
            // {
            //     result[j] = vectorA[j] * vectorB[j];
            // }

            // DateTime end = DateTime.Now;
            // Console.WriteLine("Example: {0}",counter);
            // //Console.WriteLine(string.Join(",", result));
            // TimeSpan ts = (end - start);
            // Console.WriteLine("Elapsed Time for Vectors is {0} ms", ts.TotalMilliseconds);
            // Console.WriteLine("----------------");


            // Math.Net

            Vector<float> vA= Vector<float>.Build.Dense(vectorA);
            Vector<float> vB= Vector<float>.Build.Dense(vectorB);
            //DateTime start = DateTime.Now;
            Vector<float> res =vA.PointwiseMultiply(vB);
            //DateTime end = DateTime.Now;
             var result = res.ToArray();
            // Console.WriteLine("Example: {0}", counter);
            // //Console.WriteLine(string.Join(",", res));
            // TimeSpan ts = (end - start);
            // Console.WriteLine("Elapsed Time for Math.NET is {0} ms", ts.TotalMilliseconds);
            // Console.WriteLine("----------------");
            return result;       


           


            // normal approach

            // var result2 = new float[vectorA.Length];
            // DateTime start2 = DateTime.Now;                        
            // for (var i = 0; i < vectorA.Length; i++)
            // {
            //     result2[i] = vectorA[i] * vectorB[i];
            // }
            // DateTime end2 = DateTime.Now;
            
            // //Console.WriteLine(string.Join(",", result2));
            // TimeSpan ts2 = (end2 - start2);
            // Console.WriteLine("Elapsed Time for naive is {0} ms", ts2.TotalMilliseconds);
            // Console.WriteLine("----------------");

            // counter += 1;
            

            // return result2;
        }

        public static float[][] MultiplyVectorCellsBatch(float[][] vectorAsList, float[] vectorB)
        {
            return vectorAsList.Select(vectorA => MultiplyVectorCells(vectorA, vectorB)).ToArray();

        }
    }
}
