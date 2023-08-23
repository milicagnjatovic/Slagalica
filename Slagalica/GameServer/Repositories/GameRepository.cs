using GameServer.Entities;

namespace GameServer.Repositories
{
    public class GameRepository : IGameRepository
    {
        public GameRepository() {}
        public List<Game> Games { get; } = new List<Game>();   
    }
}
