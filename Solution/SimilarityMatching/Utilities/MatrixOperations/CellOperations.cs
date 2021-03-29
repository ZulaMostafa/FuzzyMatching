// using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
//using System.Numerics;
// using System.Diagnostics;
// using NumSharp;
// using PandasNet.Impl;
// using PandasNet;

using MathNet.Numerics.LinearAlgebra;
// using MathNet.Numerics.LinearAlgebra.Double;

namespace FuzzyMatching.MatrixOperations
{
    
    public static class CellOperations
    {
        
        public static float[] MultiplyVectorCells(float[] vectorA, float[] vectorB)
        {
            
            
            


            // Math.Net

            Vector<float> vA= Vector<float>.Build.Dense(vectorA);
            Vector<float> vB= Vector<float>.Build.Dense(vectorB);
            
            Vector<float> res =vA.PointwiseMultiply(vB);
          
             var result = res.ToArray();
            
            return result;
            

            

            // normal approach

             /*var result2 = new float[vectorA.Length];
            // DateTime start2 = DateTime.Now;                        
             for (var i = 0; i < vectorA.Length; i++)
             {
                 result2[i] = vectorA[i] * vectorB[i];
             }
             return result2;*/
            // DateTime end2 = DateTime.Now;
            
            // //Console.WriteLine(string.Join(",", result2));
            // TimeSpan ts2 = (end2 - start2);
            // Console.WriteLine("Elapsed Time for naive is {0} ms", ts2.TotalMilliseconds);
            // Console.WriteLine("----------------");

            // counter += 1;
            

        }

        public static float[][] MultiplyVectorCellsBatch(float[][] vectorAsList, float[] vectorB)
        {
            
            var task = vectorAsList.AsParallel().Select( vectorA => MultiplyVectorCells(vectorA,vectorB)).ToArray();
            return task;

            // return vectorAsList.Select(vectorA => MultiplyVectorCells(vectorA, vectorB)).ToArray();

        }
    }
}
