using System.Text.Json;
using WhoKnowsKnows.GRPC.Protos;

namespace GameServer.GrpcServices
{
    public class WhoKnowsKnowsGrpcService
    {
        private readonly WhoKnowsKnowsProtoService.WhoKnowsKnowsProtoServiceClient _whoKnowsKnowsProtoServiceClient;

        public WhoKnowsKnowsGrpcService(WhoKnowsKnowsProtoService.WhoKnowsKnowsProtoServiceClient whoKnowsKnowsProtoServiceClient)
        {
            _whoKnowsKnowsProtoServiceClient = whoKnowsKnowsProtoServiceClient ?? throw new ArgumentNullException(nameof(whoKnowsKnowsProtoServiceClient));
        }

        public async Task<GetQuestionsResponse> GetQuestions()
        {
            var questionsRequest = new GetQuestionsRequest();
            return await _whoKnowsKnowsProtoServiceClient.GetQuestionsAsync(questionsRequest);
        }

        public async Task<CalculatePointsResponse> CalculatePoints(string answersJson)
        {
            var request = JsonSerializer.Deserialize<CalcualtePointsRequest>(answersJson);
            return await _whoKnowsKnowsProtoServiceClient.CalculatePointsAsync(request);
        }
    }
}
