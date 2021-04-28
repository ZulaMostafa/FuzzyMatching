using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzyMatching.Core.Adapters
{
    public static class ArraysAdapter
    {
        public static float[][] Make2DArray(float[] input, int height, int width)
        {
            float[,] output = new float[height, width];
            // i -> k/cols
            //j -> k mod cols 
            var k = 0;
            var index = 0;
            while (index < input.Length)
            {
                if (input[index] == 0)
                {
                    k += (int)input[index + 1] ;
                    index += 2;
                }
                else
                {
                    var i = (int)k / width; // floor (integer division )
                    var j = k % width; // (integer )
                    output[i, j] = input[index];
                    k++;
                    
                    index++;
                }

            }
            var result = ToJaggedArray<float>(output);
            return result;
        }
        public static float[] Make1DArray(float[][] input)
        {
            var rows = input.Length;
            var columns = input[0].Length;
            var result = new List<float>();


            // Step 2: copy 2D array elements into a 1D array.
            var i = 0;
            var j = 0;
            while (i < rows && j < columns)
            {

                if (input[i][j] == 0)
                {
                    var counter = 1;
                    while (IncrementColumnPointer(columns, rows, ref i, ref j) && input[i][j] == 0)
                    {
                        counter++;
                    }

                    result.Add(0);
                    result.Add(counter);
                }
                else
                {
                    result.Add(input[i][j]);

                    if (!IncrementColumnPointer(columns, rows, ref i, ref j))
                        break;
                }


            }
            // Step 3: return the new array.

            return result.ToArray();
        }

        /// <summary>
        /// this function increments the pointer for a matrix in a while loop
        /// </summary>
        /// <param name="length"> number of rows or columns </param>
        /// <param name="j"> pointer i or j </param>
        /// <returns></returns>
        private static bool IncrementColumnPointer(int columns, int rows, ref int i, ref int j)
        {
            j++;
            if (j >= columns)
            {
                j = 0;
                i++;
            }
            return i < rows;

        }
        private static T[][] ToJaggedArray<T>(T[,] twoDimensionalArray)
        {
            int rowsFirstIndex = twoDimensionalArray.GetLowerBound(0);
            int rowsLastIndex = twoDimensionalArray.GetUpperBound(0);
            int numberOfRows = rowsLastIndex + 1;

            int columnsFirstIndex = twoDimensionalArray.GetLowerBound(1);
            int columnsLastIndex = twoDimensionalArray.GetUpperBound(1);
            int numberOfColumns = columnsLastIndex + 1;

            T[][] jaggedArray = new T[numberOfRows][];
            for (int i = rowsFirstIndex; i <= rowsLastIndex; i++)
            {
                jaggedArray[i] = new T[numberOfColumns];

                for (int j = columnsFirstIndex; j <= columnsLastIndex; j++)
                {
                    jaggedArray[i][j] = twoDimensionalArray[i, j];
                }
            }
            return jaggedArray;
        }
    }
}
