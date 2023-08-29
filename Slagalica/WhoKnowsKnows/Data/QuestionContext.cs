using MongoDB.Driver;
using WhoKnowsKnows.Entities;

namespace WhoKnowsKnows.Data
{
    public class QuestionContext : IQuestionContext
    {
        public IMongoCollection<Question> Questions { get; }

        public QuestionContext(IConfiguration configuration) 
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase("QuestionDB");

            Questions = database.GetCollection<Question>("Questions");

            QuestionContextSeed.SeedData(Questions);
        }
    }
}
