using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GraphicModeling
{
    public class Coord
    {
        public double X { get; set; }
        public double Y { get; set; }

        public double VectorLength
        {
            get { return Math.Sqrt(X * X + Y * Y); }
        }


        public Coord() : this(0, 0) { }
        public Coord(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double DistanceTo(Coord point)
        {
            return new Coord(
                point.X - X,
                point.Y - Y)
                    .VectorLength;
        }

        public Coord Between(Coord next)
        {
            return new Coord((this.X + next.X) * 0.5, (this.Y + next.Y) * 0.5);
        }
        public Coord BetweenWithRatio(Coord next, double numerator, double denominator)
        {
            var ratio = numerator / (numerator + denominator);

            return new Coord(X + ratio * (next.X - X), Y + ratio * (next.Y - Y));
        }

        public static Coord Parse(string x, string y)
        {
            return new Coord(double.Parse(x), double.Parse(y));
        }

        public Point ToPoint()
        {
            return new Point(X, Y);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append('(');
            sb.Append(X);
            sb.Append(';');
            sb.Append(Y);
            sb.Append(')');

            return sb.ToString();
        }
    }
}
