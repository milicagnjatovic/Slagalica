namespace GameServer.Hubs
{
    public interface IGameClient
    {
        Task SendWhoKnowsKnows(string questionsJson);
        Task SendPoints(int playerPoints, int opponentPoints);
        Task SendOutcomeMessage(string message);
        Task Concede();
    }
}
