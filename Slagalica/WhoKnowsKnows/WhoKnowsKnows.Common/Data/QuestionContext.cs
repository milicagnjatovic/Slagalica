using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using WhoKnowsKnows.Common.Entities;

namespace WhoKnowsKnows.Common.Data
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
