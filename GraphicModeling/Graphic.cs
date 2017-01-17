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
                // DrawBisects(t);
                // DrawHeights(t);

                Draw(t, Brushes.Crimson, 3, Brushes.Transparent);
            }
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

        private void DrawBisects(Triangle t)
        {
            throw new NotImplementedException();
        }

        private void DrawHeights(Triangle t)
        {
            throw new NotImplementedException();
        }
    }
}