using Google.Protobuf;
using Google.Protobuf.Reflection;
using System.Reflection;
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
            Console.WriteLine("Answer JSON: " + answersJson);
            var request = Deserialize<CalcualtePointsRequest>(answersJson);
            if (request.Answers.Count == 0)
                Console.WriteLine("Answers are empty!");

            foreach (var answer in request.Answers)
                Console.WriteLine("Player answered: " + answer.Answer_);

            return await _whoKnowsKnowsProtoServiceClient.CalculatePointsAsync(request);
        }

        // Code for desirializing a JSON to a generic protobuf type.
        // Found at: https://www.c-sharpcorner.com/article/generic-implementation-for-serializerdeserializer-using-google-protocol-buffer/
        private static TResult Deserialize<TResult>(string value)
        {
            try
            {
                System.Type type = typeof(TResult);
                var typ = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == type.Name);
                var descriptor = (MessageDescriptor)typ.GetProperty("Descriptor", BindingFlags.Public | BindingFlags.Static).GetValue(null, null);
                var response = descriptor.Parser.ParseJson(value);
                return (TResult)response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
