using System;
using TicTacToe.Data.Entities;

namespace TicTacToe.Services
{
    public class GameService : IGameService
    {
        private List<Game> _games = new List<Game>();
        private List<Player> _players = new List<Player>();

        public void AddOpponent(Player opponent, string gameName)
        {
            Game game = FindGame(gameName);
            opponent.Symbol = "O";
            throw new NotImplementedException();
        }

        public Game CreateGame(Player owner, string gameName)
        {
            Game newGame = new Game(gameName);
            owner.Symbol = "X";
            owner.WaitingForMove = false;
            newGame.Owner = owner;
            _games.Add(newGame);

            return newGame;
        }

        public void DeleteGame(string gameName)
        {
            throw new NotImplementedException();
        }

        public Game FindGame(string gameName)
        {
            string upperGameName = gameName.ToUpper();

            Game game = _games.Where(g => g.Name == upperGameName).First();

            return game;
        }

        public bool GameExists(string gameName)
        {
            string upperGameName = gameName.ToUpper();

            Game game = _games.Where(g => g.Name == upperGameName).FirstOrDefault();

            return game != null;
        }

        public List<Game> GetActiveGames()
        {
            return _games.Where(g => g.State != GameStatus.Finished).ToList();
        }

        public void StartGame(string gameName)
        {
            Game game = FindGame(gameName);
            game.State = GameStatus.Playing;
        }

        public void FinishGame(string gameName)
        {
            Game game = FindGame(gameName);
            game.State = GameStatus.Finished;
        }

        public Player CreatePlayer(string name, string id)
        {
            Player existingPlayer = _players.Find(p => p.Id == id);

            if (existingPlayer == null)
            {
                Player newPlayer = new Player() { Name = name, Id = id };
                _players.Add(newPlayer);

                return newPlayer;
            }

            existingPlayer.Name = name;
            return existingPlayer;
        }

        public List<Game> GetPlayerGames(string id)
        {
            List<Game> gamesByPlayer = _games
                .Where(g => g.Owner.Id == id || g.Oponent.Id == id)
                .ToList();

            return gamesByPlayer;
        }
    }
}
