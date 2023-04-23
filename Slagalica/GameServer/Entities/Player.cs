namespace GameServer.Entities
{
    public class Player
    {
        public string ConnectionId { get; set; }
        public string Name { get; set; }

        public Player(string name = null)
        {
            Name = name;
        }
    }
}
