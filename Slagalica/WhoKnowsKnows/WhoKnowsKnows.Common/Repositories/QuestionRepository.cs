using AutoMapper;
using MongoDB.Driver;
using WhoKnowsKnows.Common.Data;
using WhoKnowsKnows.Common.Entities;
using WhoKnowsKnows.Common.Repositories.Interfaces;

namespace WhoKnowsKnows.Common.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        public IQuestionContext _context;
        private readonly IMapper _mapper;

        public long IdCount { get; set; }

        public QuestionRepository(IQuestionContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

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

        public async Task<GetQuestionDTO> GetQuestion(long numId)
        {
            var question = await _context.Questions.Find(q => q.NumId == numId).FirstOrDefaultAsync();

            return _mapper.Map<GetQuestionDTO>(question);
        }

        public async Task<bool> DeleteQuestion(string id)
        {
            var deleteResult = await _context.Questions.DeleteOneAsync(q => q.Id == id);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
