namespace GameServer.Entities
{
    public class Player
    {
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public Dictionary<string, bool> Submitted { get; set; }

        public Player(string name = null)
        {
            Name = name;
            Points = 0;

            Submitted = new Dictionary<string, bool>();
            Submitted["WhoKnowsKnows"] = false;
            Submitted["Matches"] = false;
            Submitted["Associations"] = false;
        }
    }
}
