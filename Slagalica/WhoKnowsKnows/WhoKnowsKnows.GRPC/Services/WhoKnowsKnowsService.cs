using AutoMapper;
using Grpc.Core;
using WhoKnowsKnows.Common.Entities;
using WhoKnowsKnows.Common.Repositories.Interfaces;
using WhoKnowsKnows.GRPC.Protos;
using static WhoKnowsKnows.GRPC.Protos.CalcualtePointsRequest.Types;

namespace WhoKnowsKnows.GRPC.Services
{
    public class WhoKnowsKnowsService : WhoKnowsKnowsProtoService.WhoKnowsKnowsProtoServiceBase
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<WhoKnowsKnowsService> _logger;

        public WhoKnowsKnowsService(IQuestionRepository questionRepository, IMapper mapper, ILogger<WhoKnowsKnowsService> logger)
        {
            _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<GetQuestionsResponse> GetQuestions(GetQuestionsRequest request, ServerCallContext context)
        {
            var random = new Random();
            var indices = new List<long>();
            int numOfQuestions = 10;

            for (int i = 0; i < numOfQuestions; ++i)
            {
                var newNumber = random.NextInt64(0, _questionRepository.IdCount);
                while (indices.Contains(newNumber))
                    newNumber = random.NextInt64(0, _questionRepository.IdCount);

                indices.Add(newNumber);
            }

            var questions = new List<GetQuestionDTO>();

            foreach (long index in indices)
            {
                var question = await _questionRepository.GetQuestion(index) 
                    ?? throw new RpcException(new Status(StatusCode.NotFound, $"Question with index = {index} is not found!"));

                _logger.LogInformation("Question with index {index} is retrieved", question.numId);

                questions.Add(question);
            }

            var response = new GetQuestionsResponse();
            try
            {
                _logger.LogInformation("questions list length before mapping: {questions}", questions.Count());
                response.Questions.AddRange(_mapper.Map<IEnumerable<GetQuestionsResponse.Types.Question>>(questions));
                _logger.LogInformation("Question list length after mapping: {response}", response.Questions.Count);
            } catch (Exception ex)
            {
                _logger.LogInformation("Failed to map questions list!");
            }

            return response;
        }

        public override async Task<CalculatePointsResponse> CalculatePoints(CalcualtePointsRequest request, ServerCallContext context)
        {
            int points = 0;

            var answers = new List<Answer>();

            foreach (Answer answerTuple in request.Answers)
            {
                var numId = answerTuple.NumId;
                var givenAnswer = answerTuple.Answer_;

                var originalQuestion = await _questionRepository.GetQuestion(numId);

                if (originalQuestion.CorrectAnswer == givenAnswer) { 
                    _logger.LogInformation($"Correct answer!: {givenAnswer}");
                    points += 10;
                } else
                {
                    _logger.LogInformation($"Incorrect answer!: {givenAnswer}");
                }
            }

            var response = new CalculatePointsResponse();
            response.Points = points;

            return response;
        }
    }
}
