﻿using GameServer.Entities;
using GameServer.Repositories;
using Microsoft.AspNetCore.SignalR;
using System.Reflection.Metadata.Ecma335;

namespace GameServer.Hubs
{
    public class GameHub : Hub<IGameClient>
    {
        private IGameRepository _repository;
        public int numberOfClients { get; set; }

        public GameHub(IGameRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            numberOfClients = 0;
        }

        public string GetMatches()
        {
            // Just a placeholder, will be replaced with an API call.
            return "[{\"first\": \"Michael\"  \"second\": \"Jordan\"}]";
        }

        public string GetAssociations()
        {
            // Just a placeholder, will be replaced with an API call.
            return "[{\"a\": \"liquid}\"  \"b\": \"H2O\" \"c\": \"drink\"  \"d\": \"drop\"  \"column_answer\": \"water\"]";
        }

        public string GetWhoKnowsKnows()
        {
            // Just a placeholder, will be replaced with an API call.
            return "{ " +
                "\"question\": \"what is the captial of Canada?\", " +
                "\"a\": \"Ottawa\", " +
                "\"b\": \"Montreal\", " +
                "\"c\": \"Vancouver\", " +
                "\"d\": \"Toronto\", " +
                "\"correct\": \"Ottawa\"" +
                "}";
        }

        // Function for testing program, will be removed later
        public async Task TestWhoKnowsKnows()
        {
            Console.WriteLine("Invoking Test method...");
            await Clients.All.SendWhoKnowsKnows(GetWhoKnowsKnows());
        }

        public int CalculateWhoKnowsKnows(string answers)
        {
            // Just a placeholder, will be replaced with an API call.
            return 10;
        }

        public int CalculateMatches(string answers)
        {
            // Just a placeholder, will be replaced with an API call.
            return 10;
        }

        public int CalculateAssociations(string answers)
        {
            // Just a placeholder, will be replaced with an API call.
            return 10;
        }

        public async Task Submit(string answers, Func<string, int> calculatePointsFunction, Func<string> getGameFunction, Func<string, Task> sendGameFunc, string currentGame)
        {
            var game = _repository.Games.FirstOrDefault(g => g.Player1.ConnectionId == Context.ConnectionId || g.Player2.ConnectionId == Context.ConnectionId);

            var thisPlayer = Context.ConnectionId == game.Player1.ConnectionId ? game.Player1 : game.Player2;
            var otherPlayer = Context.ConnectionId == game.Player1.ConnectionId ? game.Player2 : game.Player1;

            thisPlayer.Submitted[currentGame] = true;
            var pointsWon = calculatePointsFunction(answers);
            thisPlayer.Points += pointsWon;

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
                await Submit(answers, CalculateWhoKnowsKnows, GetMatches, Clients.Group(game.Id).SendMatches, "WhoKnowsKnows");
        }

        public async Task SubmitMatches(string answers) 
        {
            var game = _repository.Games.FirstOrDefault(g => g.Player1.ConnectionId == Context.ConnectionId || g.Player2.ConnectionId == Context.ConnectionId);
            if (game != null)
                await Submit(answers, CalculateMatches, GetAssociations, Clients.Group(game.Id).SendAssociations, "Matches");
        }

        public async Task SubmitAssociations(string answers)
        {
            var game = _repository.Games.FirstOrDefault(g => g.Player1.ConnectionId == Context.ConnectionId || g.Player2.ConnectionId == Context.ConnectionId);
            if (game != null)
                await Submit(answers, CalculateAssociations, game.getWinnerMessage, Clients.Group(game.Id).SendOutcomeMessage, "Associations");
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("Connected with client!");
            numberOfClients++;
            Console.WriteLine("Current number of clients: " + numberOfClients);
            Console.WriteLine("Current repository size: " + _repository.Games.Count);
            var game = _repository.Games.FirstOrDefault(g => !g.InProgress);
            if (game == null)
            {
                Console.WriteLine("Creating empty game...");
                game = new Game();
                game.Id = Guid.NewGuid().ToString();
                game.Player1.ConnectionId = Context.ConnectionId;
                Console.WriteLine("Adding game to repository");
                _repository.Games.Add(game);
            } else {
                Console.WriteLine("Adding player to already setup game");
                game.Player2.ConnectionId = Context.ConnectionId;
                game.InProgress = true;
            }

            Console.WriteLine("Adding player to game group");
            await Groups.AddToGroupAsync(Context.ConnectionId, game.Id);
            await base.OnConnectedAsync();

            if (game.InProgress) {
                Console.WriteLine("Sending games to clients");
                var questionsJson = GetWhoKnowsKnows();
                await Clients.Group(game.Id).SendWhoKnowsKnows(questionsJson);
            }

            Console.WriteLine("Repository size after adding game: " + _repository.Games.Count);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine("Disconnecting with client...");
            numberOfClients--;
            var game = _repository.Games.FirstOrDefault(g => g.Player1.ConnectionId == Context.ConnectionId || g.Player2.ConnectionId == Context.ConnectionId);
            if (!(game == null)) {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, game.Id);
                await Clients.Group(game.Id).Concede();
                _repository.Games.Remove(game);
            }

            await base.OnDisconnectedAsync(exception);
            Console.WriteLine("Client disconnected.");
        }
    }
}
