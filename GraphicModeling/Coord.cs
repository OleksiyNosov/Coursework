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
        private Point point;

        public double X
        {
            get { return point.X; }
            set { point.X = value; }
        }
        public double Y
        {
            get { return point.Y; }
            set { point.Y = value; }
        }

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

        public void Shift(Coord coord)
        {
            X += coord.X;
            Y += coord.Y;
        }

        public static Coord Parse(string x, string y)
        {
            return new Coord(double.Parse(x), double.Parse(y));
        }

        public Point AsPoint()
        {
            return point;
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

        #region Operator +
        public static Coord operator +(Coord left, Coord right)
        {
            return
                new Coord(
                    left.X + right.X,
                    left.Y + right.Y);
        }
        public static Coord operator +(Point left, Coord right)
        {
            return
                new Coord(
                    left.X + right.X,
                    left.Y + right.Y);
        }
        public static Coord operator +(Coord left, Point right)
        {
            return right + left;
        }
        #endregion
    }
}
