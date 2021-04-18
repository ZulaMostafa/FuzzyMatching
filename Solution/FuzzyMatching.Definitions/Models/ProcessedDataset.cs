using ProtoBuf;

namespace FuzzyMatching.Definitions.Models
{
    [ProtoContract]
    public class ProcessedDataset
    {
        [ProtoMember(1)]
        public float[][] InputSentenceDatasetTFIDFMatrix { get; set; }
        [ProtoMember(2)]
        public float[] InputSentenceDataseetAbsoluteValues { get; set; }
        [ProtoMember(3)]
        public float[] OverallDataIDFVector { get; set; }
        [ProtoMember(4)]
        public string[] AllDataUniqueNGramsVector { get; set; }
    }
}
