using GameServer.Entities;
using GameServer.Repositories;
using Microsoft.AspNetCore.SignalR;
using System.Reflection.Metadata.Ecma335;

namespace GameServer.Hubs
{
    public class GameHub : Hub<IGameClient>
    {
        private IGameRepository _repository;

        public string GetWhoKnowsKnows()
        {
            return "{ " +
                "\"question\": \"what is the captial of Canada?\", " +
                "\"a\": \"Ottawa\", " +
                "\"b\": \"Montreal\", " +
                "\"c\": \"Vancouver\", " +
                "\"d\": \"Toronto\", " +
                "\"correct\": \"Ottawa\"" +
                "}";
        }

        public override async Task OnConnectedAsync()
        {
            var game = _repository.Games.FirstOrDefault(g => !g.InProgress);
            if (game == null)
            {
                game = new Game();
                game.Id = Guid.NewGuid().ToString();
                game.Player1.ConnectionId = Context.ConnectionId;
                _repository.Games.Add(game);
            } else {
                game.Player2.ConnectionId = Context.ConnectionId;
                game.InProgress = true;
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, game.Id);
            await base.OnConnectedAsync();

            if (game.InProgress) {
                var questionsJson = GetWhoKnowsKnows();
                await Clients.Group(game.Id).SendWhoKnowsKnows(questionsJson);
            }
        }
    }
}
