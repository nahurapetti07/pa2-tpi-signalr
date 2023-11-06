using System;
namespace TicTacToe.Data.Entities
{
	public class Player
	{
		public Player()
		{
			Symbol = " ";
		}

		public string Name { get; set; }
		public string Id { get; set; }
		public bool WaitingForMove { get; set; }
		public string Symbol { get; set; }
    }
}
