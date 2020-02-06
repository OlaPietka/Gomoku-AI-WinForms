using System.Collections.Generic;

namespace Gomoku
{
    public static class Extensions
    {
        public static IEnumerable<Point> GetNeighbors(this Point p)
        {
            for (var i = -1; i <= 1; i++)
                for (var j = -1; j <= 1; j++)
                    if (!(i == 0 && j == 0))
                        yield return new Point(p.X + i, p.Y + j);
        }
    }
}
