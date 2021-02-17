using System.Linq;

namespace SimilarityMatching.Core.MatrixOperations
{
    public static class CellOperations
    {
        public static float[] MultiplyVectorCells(float[] vectorA, float[] vectorB)
        {
            var result = new float[vectorA.Length];
            for (var i = 0; i < vectorA.Length; i++)
            {
                result[i] = vectorA[i] * vectorB[i];
            }
            return result;
        }

        public static float[][] MultiplyVectorCellsBatch(float[][] vectorAsList, float[] vectorB)
        {
            return vectorAsList.Select(vectorA => MultiplyVectorCells(vectorA, vectorB)).ToArray();

        }
    }
}
