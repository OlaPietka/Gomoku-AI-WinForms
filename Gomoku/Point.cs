using SkiaSharp;
using System;

namespace Gomoku
{
    public struct Point
    {

        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        #region Operators
        public static Point operator *(Point p, int i)
           =>  new Point(p.X * i, p.Y * i);

        public static Point operator +(Point p, int i)
           => new Point(p.X + i, p.Y + i);

        public static Point operator +(Point p1, Point p2)
           => new Point(p1.X + p2.X, p1.Y + p2.Y);

        public static Point operator /(Point p, int i)
           => new Point((int)Math.Round(p.X / (double)i), (int)Math.Round(p.Y / (double)i));

        public static Point operator -(Point p1, Point p2)
           => new Point(p1.X - p2.X, p1.Y - p2.Y);

        public static Point operator -(Point p, int i)
           => new Point(p.X - i, p.Y - i);

        public static implicit operator (int x, int y)(Point p)
            => (p.X, p.Y);

        public static implicit operator SKPoint(Point p)
            => new SKPoint(p.X, p.Y);

        public static bool operator ==(Point p1, Point p2)
           => p1.X == p2.X && p1.Y == p2.Y;

        public static bool operator !=(Point p1, Point p2)
           => p1.X != p2.X || p1.Y != p2.Y;
        #endregion

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        public override bool Equals(object obj)
        {
            if (obj is Point other)
                return other == new Point(X, Y);

            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = (hashCode * -1521134295) + X.GetHashCode();
            return (hashCode * -1521134295) + Y.GetHashCode();
        }

        public override string ToString()
        {
            return $"[{X}, {Y}]";
        }
    }
}
