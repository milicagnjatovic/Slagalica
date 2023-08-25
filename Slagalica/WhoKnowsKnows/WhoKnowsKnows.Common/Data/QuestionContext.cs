using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using WhoKnowsKnows.Common.Entities;

namespace WhoKnowsKnows.Common.Data
{
    public class QuestionContext : IQuestionContext
    {
        public IMongoCollection<Question> Questions { get; }

        private readonly string connectionString = "mongodb://localhost:27017";

        public QuestionContext(IConfiguration configuration)
        {
            //var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("QuestionDB");

            Questions = database.GetCollection<Question>("Questions");

            QuestionContextSeed.SeedData(Questions);
        }
    }
}
