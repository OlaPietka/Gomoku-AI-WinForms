using System.Collections.Generic;
using System.Linq;

namespace Gomoku
{
    public enum Team
    {
        Ai,
        Player
    }

    public class Board
    {
        private readonly Dictionary<Point, Team> _stones;
        public IReadOnlyDictionary<Point, Team> Stones => _stones;

        public Board()
        {
            _stones = new Dictionary<Point, Team>();
        }

        public Board(Dictionary<Point, Team> stones)
        {
            _stones = new Dictionary<Point, Team>(stones);
        }

        public bool AddStone(Point point, Team team)
        {
            if (!_stones.ContainsKey(point))
            {
                _stones.Add(point, team);
                return true;
            }

            return false;
        }

        public void RemoveStone(Point point)
        {
            _stones.Remove(point);
        }

        public bool DidTeamWin(Team? team = null)
        {
            bool Check(int x, int y, Team t)
            {
                if (_stones.TryGetValue(new Point(x, y), out var st))
                    return st == t;
                return false;
            }

            foreach (KeyValuePair<Point, Team> s in _stones)
            {
                var checkTeam = team ?? s.Value; //team != null ? team : s.Value
                if (Enumerable.Range(0, 5).All(x => Check(s.Key.X, s.Key.Y + x, checkTeam)) //Pion
                 || Enumerable.Range(0, 5).All(x => Check(s.Key.X + x, s.Key.Y, checkTeam)) //Poziom
                 || Enumerable.Range(0, 5).All(x => Check(s.Key.X + x, s.Key.Y + x, checkTeam)) //Ukos w gore
                 || Enumerable.Range(0, 5).All(x => Check(s.Key.X + x, s.Key.Y - x, checkTeam))) //Ukos w dol
                    return true;
            }

            return false;
        }

        public bool IsWinningMove(Point point, Team team)
        {
            AddStone(point, team);
            var win = DidTeamWin(team);
            RemoveStone(point);
            return win;
        }

        public List<Point> PossibleMoves()
        {
            return _stones.Keys
                .SelectMany(p => p.GetNeighbors())
                .Distinct()
                .Where(p => !_stones.ContainsKey(p))
                .ToList();
        }
    }
}