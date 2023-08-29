using MongoDB.Driver;
using WhoKnowsKnows.Common.Entities;

namespace WhoKnowsKnows.Common.Data
{
    public interface IQuestionContext
    {
        IMongoCollection<Question> Questions { get; }
    }
}
