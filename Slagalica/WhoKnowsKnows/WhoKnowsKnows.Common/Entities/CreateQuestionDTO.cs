﻿namespace WhoKnowsKnows.Common.Entities
{
    public class CreateQuestionDTO
    {
        public string Text { get; set; }
        public List<string> Answers { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
