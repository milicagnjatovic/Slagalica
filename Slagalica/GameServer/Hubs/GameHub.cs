using GameServer.Entities;
using GameServer.GrpcServices;
using GameServer.Repositories;
using Microsoft.AspNetCore.SignalR;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace GameServer.Hubs
{
    public class GameHub : Hub<IGameClient>
    {
        private readonly IGameRepository _repository;
        private readonly WhoKnowsKnowsGrpcService _whoKnowsKnowsGrpcService;
        public int numberOfClients { get; set; }

        public GameHub(IGameRepository repository, WhoKnowsKnowsGrpcService whoKnowsKnowsGrpcService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _whoKnowsKnowsGrpcService = whoKnowsKnowsGrpcService ?? throw new ArgumentNullException(nameof(whoKnowsKnowsGrpcService));
            numberOfClients = 0;
        }

        public async Task<string> GetWhoKnowsKnows()
        {
            var response = await _whoKnowsKnowsGrpcService.GetQuestions();
            return JsonSerializer.Serialize(response).ToString();
        }

        public async Task<int> CalculateWhoKnowsKnows(string answers)
        {
            var response = await _whoKnowsKnowsGrpcService.CalculatePoints(answers);
            return response.Points;
        }

        public async Task Submit(string answers, Func<string, Task<int>> calculatePointsFunction, Func<string> getGameFunction, Func<string, Task> sendGameFunc, string currentGame)
        {
            // Find the game based on the connectionId.
            var game = _repository.Games.FirstOrDefault(g => g.Player1.ConnectionId == Context.ConnectionId || g.Player2.ConnectionId == Context.ConnectionId);

            // Find which player of the two has submitted the game.
            var thisPlayer = Context.ConnectionId == game.Player1.ConnectionId ? game.Player1 : game.Player2;
            var otherPlayer = Context.ConnectionId == game.Player1.ConnectionId ? game.Player2 : game.Player1;

            // Check the field that the game is submitted.
            thisPlayer.Submitted[currentGame] = true;

            // Calculate the points won in the game and add it to his score.
            var pointsWon = await calculatePointsFunction(answers);
            Console.WriteLine("Player: " + thisPlayer.Name + "won " + pointsWon + " points.");
            thisPlayer.Points += pointsWon;

            // If the other player has sbumitted his game also, get the next game or calculate the winner.
            if (otherPlayer.Submitted[currentGame])
            {
                await Clients.Client(thisPlayer.ConnectionId).SendPoints(thisPlayer.Points, otherPlayer.Points);
                await Clients.Client(otherPlayer.ConnectionId).SendPoints(otherPlayer.Points, thisPlayer.Points);

                var nextGameJson = getGameFunction();
                await sendGameFunc(nextGameJson);
            }
        }

        public async Task SubmitWhoKnowsKnows(string answers)
        {
            var game = _repository.Games.FirstOrDefault(g => g.Player1.ConnectionId == Context.ConnectionId || g.Player2.ConnectionId == Context.ConnectionId);
            if (game != null)
                await Submit(answers, CalculateWhoKnowsKnows, game.getWinnerMessage, Clients.Group(game.Id).SendOutcomeMessage, "WhoKnowsKnows");
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("Connected with client!");
            numberOfClients++;
            Console.WriteLine("Current number of clients: " + numberOfClients);
            Console.WriteLine("Current repository size: " + _repository.Games.Count);
            // Find if there is someone waiting for a match.
            var game = _repository.Games.FirstOrDefault(g => !g.InProgress);
            // If not, create a new game.
            if (game == null)
            {
                Console.WriteLine("Creating empty game...");
                game = new Game();
                game.Id = Guid.NewGuid().ToString();
                game.Player1.ConnectionId = Context.ConnectionId;
                Console.WriteLine("Adding game to repository");
                _repository.Games.Add(game);
            } else {
                // If yes, then add the connected player to that game and set the game in progress.
                Console.WriteLine("Adding player to already setup game");
                game.Player2.ConnectionId = Context.ConnectionId;
                game.InProgress = true;
            }

            Console.WriteLine("Adding player to game group");
            // Add him to the group.
            await Groups.AddToGroupAsync(Context.ConnectionId, game.Id);
            await base.OnConnectedAsync();

            // If the game is initiated send the game to the clients.
            if (game.InProgress) {
                Console.WriteLine("Sending games to clients");
                var questionsJson = await GetWhoKnowsKnows();
                await Clients.Group(game.Id).SendWhoKnowsKnows(questionsJson);
            }

            Console.WriteLine("Repository size after adding game: " + _repository.Games.Count);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine("Disconnecting with client...");
            numberOfClients--;
            // Find the game the player disconnected on.
            var game = _repository.Games.FirstOrDefault(g => g.Player1.ConnectionId == Context.ConnectionId || g.Player2.ConnectionId == Context.ConnectionId);
            if (!(game == null)) {
                // Remove the player from the group.
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, game.Id);
                // Concede the game and remove it from the repository.
                await Clients.Group(game.Id).Concede();
                _repository.Games.Remove(game);
            }

            await base.OnDisconnectedAsync(exception);
            Console.WriteLine("Client disconnected.");
        }
    }
}
