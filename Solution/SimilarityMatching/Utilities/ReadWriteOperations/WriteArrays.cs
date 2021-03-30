using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace FuzzyMatching.ReadWriteOperations
{
    public static class WriteArrays
    {
        public static void WriteArrayInFile(float[] arr, string FileName, string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Path.Combine(path, FileName), FileMode.Create, FileAccess.Write);

            formatter.Serialize(stream, arr);
            stream.Close();


        }
        public static void WriteArrayInFile(float[][] matrix, string FileName, string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Path.Combine(path, FileName), FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, matrix);
            stream.Close();


        }

        public static void WriteArrayInFile(string[] arr, string FileName, string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Path.Combine(path, FileName), FileMode.Create, FileAccess.Write);

            formatter.Serialize(stream, arr);
            stream.Close();

        }
    }
}
