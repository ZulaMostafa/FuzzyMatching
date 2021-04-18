namespace FuzzyMatching.Definitions.Models
{
    public class FuzzyMatchingResult
    {
        public string ClosestSentence { get; set; }
        public float MatchingScore { get; set; }
        public int MatchingIndex { get; set; }
    }
}
