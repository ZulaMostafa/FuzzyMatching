using System.Collections.Generic;

namespace FuzzyMatching.Definitions.Models
{
    public class PreprocessedDataset
    {
        private float[][] inputSentenceDatasetTFIDFMatrix;
        private float[] inputSentenceDataseetAbsoluteValues;
        private float[] overallDataIDFVector;
        private string[] allDataUniqueNGramsVector;
        

        public float[][] InputSentenceDatasetTFIDFMatrix { get => inputSentenceDatasetTFIDFMatrix; set => inputSentenceDatasetTFIDFMatrix = value; }
        public float[] InputSentenceDataseetAbsoluteValues { get => inputSentenceDataseetAbsoluteValues; set => inputSentenceDataseetAbsoluteValues = value; }
        public float[] OverallDataIDFVector { get => overallDataIDFVector; set => overallDataIDFVector = value; }
        public string[] AllDataUniqueNGramsVector { get => allDataUniqueNGramsVector; set => allDataUniqueNGramsVector = value; }
        
    }
}
