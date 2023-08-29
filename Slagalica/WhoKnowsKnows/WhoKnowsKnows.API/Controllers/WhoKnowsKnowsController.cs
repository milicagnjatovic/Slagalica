using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using WhoKnowsKnows.Common.Entities;
using WhoKnowsKnows.Common.Repositories.Interfaces;

namespace WhoKnowsKnows.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WhoKnowsKnowsController : ControllerBase
    {
        private readonly IQuestionRepository _questionRepository;

        public WhoKnowsKnowsController(IQuestionRepository repository)
        {
            _questionRepository = repository ?? throw new ArgumentNullException(nameof(repository));   
        }

        [HttpGet(Name = "GetQuestions")]
        [ProducesResponseType(typeof(IEnumerable<GetQuestionDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GetQuestionDTO>>> GetQuestions()
        {

            var random  =  new Random();
            var indices = new List<long>();
            int numOfQuestions = 10;

            for (int i=0; i<numOfQuestions; ++i)
            {
                var newNumber = random.NextInt64(0, _questionRepository.IdCount);
                while (indices.Contains(newNumber))
                    newNumber = random.NextInt64(0, _questionRepository.IdCount);

                indices.Add(newNumber);
            }

            var questions = new List<GetQuestionDTO>();

            foreach (long index in indices)
            {
                var question = await _questionRepository.GetQuestion(index);
                questions.Add(question);
            }

            return questions;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Int32),  StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> CalculatePoints([FromBody] IEnumerable<AnswerTuple> answers)
        {
            int points = 0;

            foreach (AnswerTuple answerTuple in answers)
            {
                var numId = answerTuple.NumId;
                var givenAnswer = answerTuple.Answer;

                var originalQuestion = await _questionRepository.GetQuestion(numId);

                if (originalQuestion.CorrectAnswer == givenAnswer)
                    points += 10;
            }

            return points;
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Question>), StatusCodes.Status201Created)]
        public async Task<ActionResult<Question>> CreateQuestion([FromBody] CreateQuestionDTO questionDto)
        {
            if (questionDto.Answers.Count != 3)
                throw new InvalidDataException("Answers list must have 3 elements!");

            var question = new Question { 
                NumId = _questionRepository.IdCount, 
                Id = ObjectId.GenerateNewId().ToString(), 
                Text = questionDto.Text,
                Answers = questionDto.Answers,
                CorrectAnswer = questionDto.CorrectAnswer 
            };

            _questionRepository.IdCount++;

            await _questionRepository.CreateQuestion(question);

            return Created("GetQuestions", question);
    
        }
    }
}