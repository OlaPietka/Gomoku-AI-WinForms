using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Gomoku
{
    public partial class Form1 : Form
    {
        private readonly Ai ai = new Ai();

        private Board board = new Board();
        private bool isMouseDown;
        private Point dragMove = new Point();
        private Point camera = new Point();
        private Point dragStart;
        private int stoneSize = 50;

        private Team Current { get; set; }
        private Team Next => Current == Team.Ai ? Team.Player : Team.Ai;

        private int bPoints;
        private int pPoints;

        private readonly Dictionary<Team, SKPaint> teamColors = new Dictionary<Team, SKPaint>();

        public Form1()
        {
            InitializeComponent();

            skControl.MouseWheel += (s, e) =>
            {
                stoneSize += e.Delta / 25;
                stoneSize = Math.Max(Math.Min(stoneSize, 150), 10);
            };

            blackPoints.Text = $"Black player points: {bPoints}";
            pinkPoints.Text = $"Pink player points: {pPoints}";

            teamColors.Add(Team.Ai,
                new SKPaint
                {
                    Color = SKColors.Black,
                    IsAntialias = true,
                    Style = SKPaintStyle.Fill,
                });

            teamColors.Add(Team.Player,
                new SKPaint
                {
                    Color = SKColors.Purple,
                    IsAntialias = true,
                    Style = SKPaintStyle.Fill,
                });

            Current = Team.Player;
        }

        private void PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.LightGoldenrodYellow);

            var paint = new SKPaint
            {
                Color = SKColors.Black,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
            };

            var translate = camera + dragMove;

            var width = skControl.Size.Width;
            var height = skControl.Size.Height;

            canvas.Translate(new SKPoint(-translate.X, -translate.Y));

            var from = WorldToIndex(translate);
            var to = WorldToIndex(translate + new Point(width, height));
            
            for(var x = from.X - 1; x < to.X + 1; x++)
                canvas.DrawLine(IndexToWorld(new Point(x, from.Y - 1)),
                                IndexToWorld(new Point(x, to.Y + 1)), paint);

            for (var y = from.Y - 1; y < to.Y + 1; y++)
                canvas.DrawLine(IndexToWorld(new Point(from.X - 1, y)),
                                IndexToWorld(new Point(to.X + 1, y)), paint);

            foreach (KeyValuePair<Point, Team> s in board.Stones)
            {
                canvas.DrawCircle(IndexToWorld(s.Key) - (stoneSize / 2), stoneSize / 2, teamColors[s.Value]);
                canvas.DrawText($"{s.Key.X}, {s.Key.Y}", IndexToWorld(s.Key) - new Point(stoneSize*3/4, stoneSize/2), new SKPaint() { Color = SKColors.White});
            }

            skControl.Invalidate();
        }

        private void SkControl_DoubleClick(object sender, MouseEventArgs e)
        {
            bool AddStone(Point point)
            {
                if (board.AddStone(point, Current))
                {
                    Current = Next;
                    return true;
                }
                return false;
            }

            if (!AddStone(WorldToIndex(ScreenToWorld(e.X + (stoneSize / 2), e.Y + (stoneSize / 2)))))
                return;

            AddStone(ai.GetMove(board));

            if (board.DidTeamWin())
            {
                if (board.DidTeamWin(Team.Ai))
                    blackPoints.Text = $"Black player points: {++bPoints}";
                else
                    pinkPoints.Text = $"Pink player points: {++pPoints}";

                board = new Board();
            }
        }

        private Point ScreenToWorld(int x, int y) => new Point(x + camera.X, y + camera.Y);
        private Point WorldToIndex(Point w) => w / stoneSize;
        private Point IndexToWorld(Point i) => i * stoneSize;

        private void SkControl_MouseDown(object sender, MouseEventArgs e)
        {
            dragStart = new Point(e.X, e.Y);
            isMouseDown = true;
        }

        private void SkControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
                dragMove = dragStart - new Point(e.X, e.Y);
        }

        private void SkControl_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            camera += dragMove;
            dragMove = new Point();
        }
    }
}
