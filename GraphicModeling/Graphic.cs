using System;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Collections.Generic;

namespace GraphicModeling
{
    public class Graphic
    {
        private Canvas canvas;

        public Graphic(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public void Draw(Shape shape, Brush stroke, double strokeThickness, Brush fill)
        {
            var polyline = new Polyline();

            polyline.Stroke = stroke;
            polyline.StrokeThickness = strokeThickness;
            polyline.Fill = fill;

            foreach (var c in shape.Coords)
                polyline.Points.Add(new System.Windows.Point(c.X, c.Y));
            polyline.Points.Add(new System.Windows.Point(shape.Coords[0].X, shape.Coords[0].Y));

            canvas.Children.Add(polyline);
        }

        public void Draw(Shape shape)
        {
            Draw(shape, Brushes.Black, 2, Brushes.White);
        }

        public void Draw(List<Shape> shapes)
        {
            shapes.ForEach(s => Draw(s));
        }

        public void DrawFull(List<Triangle> triangles)
        {
            foreach (var t in triangles)
            {
                DrawMedians(t);
                DrawBisects(t);
                // DrawHeights(t);

                Draw(t, Brushes.Crimson, 3, Brushes.Transparent);
            }
        }        

        private void DrawLine(Coord coord1, Coord coord2, Brush stroke)
        {
            canvas.Children.Add(
                new Line()
                {
                    Stroke = stroke,
                    X1 = coord1.X,
                    Y1 = coord1.Y,
                    X2 = coord2.X,
                    Y2 = coord2.Y
                });
        }

        public void DrawFull(Quadrangle q)
        {
            DrawDiagonals(q);

            Draw(q, Brushes.DodgerBlue, 3, Brushes.Transparent);
        }

        private void DrawDiagonals(Quadrangle q)
        {
            DrawDiagonal(q, 0);
            DrawDiagonal(q, 1);
        }

        private void DrawDiagonal(Quadrangle q, int i)
        {
            DrawLine(q.Coords[i], q.Coords[(i + 2) % 4], Brushes.MediumPurple);
        }

        private void DrawMedians(Triangle t)
        {
            for (int i = 0; i < 3; i++)
                DrawMedian(t, i);
        }

        private void DrawMedian(Triangle t, int i)
        {
            DrawLine(
                t.Coords[i],
                t.Coords[(i + 1) % 3].Between(t.Coords[(i + 2) % 3]),
                Brushes.Orange);
        }

        private void DrawBisects(Triangle t)
        {
            for (int i = 0; i < 3; i++)
                DrawBisect(t, i);
        }

        private void DrawBisect(Triangle t, int i)
        {
            DrawLine(
                t.Coords[i],
                t.Coords[(i + 1) % 3].BetweenWithRatio(
                    t.Coords[(i + 2) % 3],
                    t.Coords[i].DistanceTo(t.Coords[(i + 1) % 3]),
                    t.Coords[(i + 2) % 3].DistanceTo(t.Coords[i])),
                Brushes.Green);
        }

        private void DrawHeights(Triangle t)
        {
            for (int i = 0; i < 3; i++)
                DrawHeight(t, i);
        }

        private void DrawHeight(Triangle t, int i)
        {
            throw new NotImplementedException();
        }
    }
}