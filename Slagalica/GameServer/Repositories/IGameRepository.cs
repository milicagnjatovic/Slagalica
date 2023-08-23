using GameServer.Entities;

namespace GameServer.Repositories
{
    public interface IGameRepository
    {
        List<Game> Games { get; }
    }
}
