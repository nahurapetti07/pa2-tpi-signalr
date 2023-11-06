using System;
using TicTacToe.Data.Entities;

namespace TicTacToe.Services
{
	public interface IGameService
	{
        Game CreateGame(Player owner, string gameName);
        void StartGame(string gameName);
        void FinishGame(string gameName);
        void DeleteGame(string gameName);

        Player CreatePlayer(string name, string id);
        List<Game> GetPlayerGames(string id);

        void AddOpponent(Player opponent, string gameName);
        Game FindGame(string gameName);
        bool GameExists(string gameName);

        List<Game> GetActiveGames();
    }
}

