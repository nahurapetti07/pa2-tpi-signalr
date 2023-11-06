using System;
using System.Numerics;

namespace TicTacToe.Data.Entities
{
    public class Board
    {
        private readonly string[] _board = new string[9];

        public Board()
        {
        }

        public Board(string symbol)
        {
            for (int i = 0; i < 9; i++)
            {
                _board[i] = symbol;
            }
        }

        public bool IsWinner()
        {
            // Chequeo lineas horizontales
            if (_board[0] == _board[1] && _board[1] == _board[2] && _board[2] != " ")
                return true;
            if (_board[3] == _board[4] && _board[4] == _board[5] && _board[5] != " ")
                return true;
            if (_board[6] == _board[7] && _board[7] == _board[8] && _board[8] != " ")
                return true;

            // Chequeo lineas verticales
            if (_board[0] == _board[3] && _board[3] == _board[6] && _board[6] != " ")
                return true;
            if (_board[1] == _board[4] && _board[4] == _board[7] && _board[7] != " ")
                return true;
            if (_board[2] == _board[5] && _board[5] == _board[8] && _board[8] != " ")
                return true;

            // Chequeo lineas diagonales
            if (_board[0] == _board[4] && _board[4] == _board[8] && _board[8] != " ")
                return true;
            if (_board[2] == _board[4] && _board[4] == _board[6] && _board[6] != " ")
                return true;

            // Default
            return false;
        }

        public bool IsPlayed(char input)
        {
            return input == 'X' || input == 'O';
        }

        public bool IsDraw()
        {
            for (int i = 0; i < 9; i++)
        {
                
                if (_board[i] == " ")
                {
                    return false;
                }
            }

            return true;
        }

        public void MakeMove(int index, string player)
        {
            _board[index] = player;
        }

        public string ShowCell(int index)
        {
            return _board[index];
        }
    }
}

