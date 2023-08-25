using WhoKnowsKnows.Common.Entities;
using WhoKnowsKnows.Common.Extensions;
using WhoKnowsKnows.GRPC.Protos;
using WhoKnowsKnows.GRPC.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddWhoKnowsKnowsCommonServices();

builder.Services.AddAutoMapper(configuration =>
{
    configuration.CreateMap<GetQuestionDTO, GetQuestionsResponse.Types.Question>().ReverseMap();
    configuration.CreateMap<AnswerTuple, CalcualtePointsRequest.Types.Answer>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<WhoKnowsKnowsService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
