using MongoDB.Driver;
using WhoKnowsKnows.Entities;

namespace WhoKnowsKnows.Data
{
    public interface IQuestionContext
    {
        IMongoCollection<Question> Questions { get; }
    }
}
