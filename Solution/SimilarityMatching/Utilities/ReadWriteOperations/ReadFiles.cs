using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FuzzyMatching.ReadWriteOperations
{
    public static class ReadFiles
    {
        public static float[] ReadFloatArrayFromFile(string FileName, string path)
        {
            var array = new List<float>();
            using (BinaryReader br = new BinaryReader(File.Open(Path.Combine(path, FileName), FileMode.Open)))
            {
                long size = br.BaseStream.Length;
                while (br.BaseStream.Position != size)
                {
                    array.Add(br.ReadSingle());
                }
            }
            return array.ToArray();
        }

        public static float[][] Read2DFloatArrayFromFile(string FileName,string path)
        {
            var array = new List<float[]>();
            using (BinaryReader br = new BinaryReader(File.Open(Path.Combine(path, FileName), FileMode.Open)))
            {
                var temp = new List<float>();
                long size = br.BaseStream.Length;
                float value;
                while (br.BaseStream.Position != size)
                {
                    value = br.ReadSingle();
                    if (value == -1.1F)
                    {
                        array.Add(temp.ToArray());
                        temp.Clear();
                        continue;

                    }
                    temp.Add(value);



                }
            }
            return array.ToArray();
        }

        public static string[] ReadStringArrayFromFile(string FileName,string path)
        {
            var array = new List<string>();
            using (BinaryReader br = new BinaryReader(File.Open(Path.Combine(path, FileName), FileMode.Open)))
            {
                long size = br.BaseStream.Length;
                while (br.BaseStream.Position != size)
                {
                    array.Add(br.ReadString());
                }
            }
            return array.ToArray();

        }

    }
}
