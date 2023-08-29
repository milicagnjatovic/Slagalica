namespace WhoKnowsKnows.Entities
{
    public class QuestionDTO
    {
        public string Text { get; set; }
        public List<string> Answers { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
