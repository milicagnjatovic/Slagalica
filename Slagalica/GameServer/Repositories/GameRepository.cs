using GameServer.Entities;

namespace GameServer.Repositories
{
    public class GameRepository : IGameRepository
    {
        public List<Game> Games { get; } = new List<Game>();   
        public GameRepository() {}
    }
}
