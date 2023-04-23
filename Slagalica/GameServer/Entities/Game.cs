namespace GameServer.Entities
{
    public class Game
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public string Id { get; set; }
        public bool InProgress { get; set; }

        public Game() { 
            Player1 = new Player();
            Player2 = new Player();
            InProgress = false;
        }

        public Game(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
            InProgress = false;
        }
    }
}
