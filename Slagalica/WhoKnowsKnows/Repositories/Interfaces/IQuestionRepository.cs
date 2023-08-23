using MongoDB.Bson;
using WhoKnowsKnows.Entities;

namespace WhoKnowsKnows.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        public long IdCount { get; set; }

        Task CreateQuestion(Question question);

        Task<Question> GetQuestion(string id);

        Task<Question> GetQuestion(long numId);

        Task<bool> DeleteQuestion(string id);

    }
}
