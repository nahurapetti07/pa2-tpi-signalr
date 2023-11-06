using System;
namespace TicTacToe.Data.Entities
{
	public class Game
	{
		public Game() { }
		public Game(string gameName)
		{
			Name = gameName;
			State = GameStatus.WaitingOpponent;
            Board = new string[] { " ", " ", " ", " ", " ", " ", " ", " ", " " };
            Messages = new List<Message>();
        }

        // public Board Board { get; set; }
		public string Name { get; set; }
        public Player Owner { get; set; }
		public Player Oponent { get; set; }
        public Player Winner { get; set; }
		public GameStatus State { get; set; }
        public string[] Board { get; set; }
        public List<Message> Messages { get; set; }

        public bool IsWinner()
        {
            // Chequeo lineas horizontales
            if (Board[0] == Board[1] && Board[1] == Board[2] && Board[2] != " ")
                return true;
            if (Board[3] == Board[4] && Board[4] == Board[5] && Board[5] != " ")
                return true;
            if (Board[6] == Board[7] && Board[7] == Board[8] && Board[8] != " ")
                return true;

            // Chequeo lineas verticales
            if (Board[0] == Board[3] && Board[3] == Board[6] && Board[6] != " ")
                return true;
            if (Board[1] == Board[4] && Board[4] == Board[7] && Board[7] != " ")
                return true;
            if (Board[2] == Board[5] && Board[5] == Board[8] && Board[8] != " ")
                return true;

            // Chequeo lineas diagonales
            if (Board[0] == Board[4] && Board[4] == Board[8] && Board[8] != " ")
                return true;
            if (Board[2] == Board[4] && Board[4] == Board[6] && Board[6] != " ")
                return true;

            // Default
            return false;
        }

        public bool IsPlayed(string input)
        {
            return input == "X" || input == "O";
        }

        public bool IsDraw()
        {
            for (int i = 0; i < 9; i++)
            {
                if (Board[i] == " ")
                {
                    return false;
                }
            }
            return true;
        }

        public void MakeMove(int index, string player)
        {
            Board[index] = player;
        }

        public string ShowCell(int index)
        {
            return Board[index];
        }
    }
}
