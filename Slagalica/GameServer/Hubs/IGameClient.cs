namespace GameServer.Hubs
{
    public interface IGameClient
    {
        Task SendWhoKnowsKnows(string questionsJson);
        Task SendMatches(string matchesJson);
        Task SendAssociations(string associationsJson);
        Task SendPoints(int playerPoints, int opponentPoints);
        Task SendOutcomeMessage(string message);
        Task Concede();
    }
}
