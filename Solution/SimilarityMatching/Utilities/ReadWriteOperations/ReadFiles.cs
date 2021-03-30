using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace FuzzyMatching.ReadWriteOperations
{
    public static class ReadFiles
    {
        public static float[] ReadFloatArrayFromFile(string FileName, string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Path.Combine(path, FileName), FileMode.Open, FileAccess.Read);
            var array = (float []) formatter.Deserialize(stream);
            return array;
        }

        public static float[][] Read2DFloatArrayFromFile(string FileName,string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Path.Combine(path, FileName), FileMode.Open, FileAccess.Read);
            var array = (float[][])formatter.Deserialize(stream);
            return array;
        }

        public static string[] ReadStringArrayFromFile(string FileName,string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(Path.Combine(path, FileName), FileMode.Open, FileAccess.Read);
            var array = (string[])formatter.Deserialize(stream);
            return array;

        }

    }
}
