using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gomoku
{
    internal class Ai
    {
        public Point GetMove(Board board)
        {
            if (board.Stones.Count >= 5)
            {
                var (WinningTeam, Move) = BestMove(board);
                if (WinningTeam != null)
                    return Move;
            }

            return RandomWeightedMove(board);
        }

        private (Team? WinningTeam, Point Move) BestMove(Board board)
        {
            (bool Win, Point Move) ai = (false, new Point());
            (bool Win, Point Move) player = (false, new Point());

            var _possibleMoves = new Stack<Point>(board.PossibleMoves());
            while (_possibleMoves.Count > 0)
            {
                var possibleMove = _possibleMoves.Pop();

                if (board.IsWinningMove(possibleMove, Team.Ai))
                {
                    ai = (true, possibleMove);
                    break;
                }

                if (board.IsWinningMove(possibleMove, Team.Player))
                    player = (true, possibleMove);
            }

            if (!ai.Win && !player.Win)
            {
                _possibleMoves = new Stack<Point>(board.PossibleMoves());

                bool CheckMove(Point point, Team team)
                {
                    board.AddStone(point, team);

                    var winningMove = _possibleMoves
                        .Concat(point.GetNeighbors().Where(p => !board.Stones.ContainsKey(p)))
                        .Any(p => board.IsWinningMove(p, team));

                    board.RemoveStone(point);
                    return winningMove;
                }

                while (_possibleMoves.Count > 0)
                {
                    var possibleMove = _possibleMoves.Pop();

                    if (CheckMove(possibleMove, Team.Ai))
                        ai = (true, possibleMove);

                    if (CheckMove(possibleMove, Team.Player))
                        player = (true, possibleMove);
                }
            }

            if (!ai.Win && !player.Win)
                return (null, new Point());
            if (ai.Win || (ai.Win && player.Win))
                return (Team.Ai, ai.Move);
            return (Team.Player, player.Move);
        }

        private Point RandomWeightedMove(Board board)
        {
            int SimulateBoard(Point point)
            {
                const int roundLimit = 10;

                var random = new Random();
                var clonedBoard = new Board(board.Stones.ToDictionary(s => s.Key, s => s.Value));
                var currentTeam = Team.Ai;
                var round = 0;

                clonedBoard.AddStone(point, Team.Ai);

                while (!clonedBoard.DidTeamWin() && round < roundLimit)
                {
                    var possibleMoves = clonedBoard.PossibleMoves();
                    int randomIndex = random.Next(possibleMoves.Count);
                    clonedBoard.AddStone(possibleMoves[randomIndex], currentTeam);

                    currentTeam = currentTeam == Team.Ai ? Team.Player : Team.Ai;
                    round++;
                }

                if (round >= roundLimit)
                    return 1000;

                return clonedBoard.DidTeamWin(Team.Ai) ? round : round * round;
            }

            var moves = new List<(int Weight, Point Point)>();
            foreach(var point in board.PossibleMoves())
            {
                var weight = 0;
                for (var i = 0; i < 10; i++)
                    weight += SimulateBoard(point);

                moves.Add((weight, point));
            }
            
            return moves
                .MinBy(m => m.Weight)
                .Shuffle()
                .First().Point;
        }
    }
}
