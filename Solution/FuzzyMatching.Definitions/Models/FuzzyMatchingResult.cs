namespace FuzzyMatching.Definitions.Models
{
    public class FuzzyMatchingResult
    {
        private string closestSentence;
        private float matchingScore;
        private int matchingIndex;

        public string ClosestSentence { get => closestSentence; set => closestSentence = value; }
        public float MatchingScore { get => matchingScore; set => matchingScore = value; }
        public int MatchingIndex { get => matchingIndex; set => matchingIndex = value; }
    }
}
