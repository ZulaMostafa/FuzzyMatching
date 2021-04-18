namespace FuzzyMatching.Definitions.Services
{
    public interface IStorageService
    {
        public void StoreBinaryObject<T>(T data, string fileName, string relativePath);
        public T LoadBinaryObject<T>(string fileName, string relativePath);
        public string[] ListPreprocessedDatasets(string relativePath);
    }
}
