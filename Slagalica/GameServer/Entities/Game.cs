namespace GameServer.Entities
{
    public enum Outcome 
    {
        Player1,
        Player2,
        Draw
    }

    public class Game
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public string Id { get; set; }
        public bool InProgress { get; set; }
        public Outcome Winner { get; set; }

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

        private void CalculateWinner()
        {
            if (Player1.Points > Player2.Points)
                Winner = Outcome.Player1;
            else if (Player1.Points < Player2.Points)
                Winner = Outcome.Player2;
            else
                Winner = Outcome.Draw;
        }

        public string getWinnerMessage()
        {
            CalculateWinner();

            string winnerMessage;

            if (Winner == Outcome.Player1)
                winnerMessage = "The winner is: " + Player1.Name + "!";
            else if (Winner == Outcome.Player2)
                winnerMessage = "The winner is: " + Player2.Name + "!";
            else
                winnerMessage = "It's a draw!";

            return winnerMessage;
        }
    }
}
