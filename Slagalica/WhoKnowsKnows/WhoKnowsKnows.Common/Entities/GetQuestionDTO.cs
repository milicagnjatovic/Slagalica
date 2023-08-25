namespace WhoKnowsKnows.Common.Entities
{
    public class GetQuestionDTO
    {
        public long numId { get; set; }
        public string Text { get; set; }
        public List<string> Answers { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
