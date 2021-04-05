namespace FuzzyMatching.Definitions.Models
{
    public class PreprocessedDataset
    {
        public float[][] InputSentenceDatasetTFIDFMatrix;
        public float[] InputSentenceDataseetAbsoluteValues;
        public float[] OverallDataIDFVector;
        public string[] AllDataUniqueNGramsVector;
    }
}
