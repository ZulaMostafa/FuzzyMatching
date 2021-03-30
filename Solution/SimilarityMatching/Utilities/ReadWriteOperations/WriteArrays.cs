using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FuzzyMatching.ReadWriteOperations
{
    public static class WriteArrays
    {
        public static void WriteFloatArrayInFile(float[] arr, string FileName, string path)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(Path.Combine(path, FileName), FileMode.Create)))
            {
                foreach (var element in arr)
                {
                    bw.Write(element);

                }
            }


        }
        public static void WriteFloatArrayInFile(float[][] matrix, string FileName, string path)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(Path.Combine(path, FileName), FileMode.Create)))
            {
                foreach (var array in matrix)
                {
                    foreach (var element in array)
                    {
                        bw.Write(element);
                    }
                    bw.Write(-1.1F);
                }

            }


        }

        public static void WriteStringArrayInFile(string[] arr, string FileName, string path)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(Path.Combine(path, FileName), FileMode.Create)))
            {
                foreach (var element in arr)
                {
                    bw.Write(element);
                }
            }

        }
    }
}
