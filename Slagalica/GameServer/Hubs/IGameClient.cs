namespace GameServer.Hubs
{
    public interface IGameClient
    {
        Task SendWhoKnowsKnows(string questionsJson);
    }
}
