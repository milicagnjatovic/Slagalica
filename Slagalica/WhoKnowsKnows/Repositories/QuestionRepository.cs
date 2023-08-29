using WhoKnowsKnows.Data;
using WhoKnowsKnows.Entities;
using WhoKnowsKnows.Repositories.Interfaces;

using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;

namespace WhoKnowsKnows.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        public IQuestionContext _context;

        public long IdCount { get; set; }

        public QuestionRepository(IQuestionContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            IdCount = _context.Questions.CountDocuments(Builders<Question>.Filter.Empty);
        }

        public async Task CreateQuestion(Question question)
        {
            await _context.Questions.InsertOneAsync(question);
        }

        public async Task<Question> GetQuestion(string id)
        {
            return await _context.Questions.Find(q => q.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Question> GetQuestion(long numId)
        {
            return await _context.Questions.Find(q => q.NumId == numId).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteQuestion(string id)
        {
            var deleteResult = await _context.Questions.DeleteOneAsync(q => q.Id == id);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
