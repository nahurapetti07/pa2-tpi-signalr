using System;
using Microsoft.AspNetCore.SignalR;
using TicTacToe.Data.Entities;
using TicTacToe.Services;

namespace TicTacToe.Blazor.Hubs
{
    public class TicTacToeHub : Hub
    {
        private readonly IGameService _gameService;
        public TicTacToeHub(IGameService gameService)
        {
            _gameService = gameService;
        }

        private static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task AddToGame(string gameName, string username)
        {
            Player opponent = _gameService.CreatePlayer(username, Context.ConnectionId);
            opponent.Symbol = "O";
            opponent.WaitingForMove = true;
            Game game = _gameService.FindGame(gameName);
            game.State = GameStatus.Playing;
            game.Oponent = opponent;
            await Groups.AddToGroupAsync(Context.ConnectionId, gameName);
            await Clients.Group(gameName).SendAsync("GameUpdated", game);
        }

        public async Task FindGame(string gameName)
        {
            bool existsGame = _gameService.GameExists(gameName);

            await Clients.Caller.SendAsync("GameExists", existsGame);
        }

        public async Task CreateGroup(string username)
        {
            string gameName = RandomString(6);
            Player owner = _gameService.CreatePlayer(username, Context.ConnectionId);
            Game game = _gameService.CreateGame(owner, gameName);
            await Groups.AddToGroupAsync(Context.ConnectionId, gameName);

            await Clients.Group(gameName).SendAsync("GroupCreated", game);
            //await Clients.Group(gameName).SendAsync("BoardUpdated", game.Board);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SetWinner(string gameName,Player player)
        {
            Game game = _gameService.FindGame(gameName);
            game.Winner = player;
            await Clients.Group(gameName).SendAsync("GameUpdated", game);
        }

        public async Task OnMakeMoveAsync(string gameName, string[] board, bool isOwner)
        {
            Game game = _gameService.FindGame(gameName);
            game.Board = board;

            game.Owner.WaitingForMove = !game.Owner.WaitingForMove;
            if (game.Oponent != null)
                game.Oponent.WaitingForMove = !game.Oponent.WaitingForMove;

            await Clients.Group(gameName).SendAsync("GameUpdated", game);
        }

        public async Task SendMessage(string gameName, string user, string message)
        {
            Game game = _gameService.FindGame(gameName);
            game.Messages.Add(new Message() { User = user, Text = message });

            await Clients.Group(gameName).SendAsync("GameUpdated", game);
        }
    }
}
