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

        public Coord Between(Coord next, double ratio = 0.5)
        {
            return new Coord((this.X + next.X) * ratio, (this.Y + next.Y) * ratio);
        }
        
        public static Coord Parse(string x, string y)
        {
            return new Coord(double.Parse(x), double.Parse(y));
        }
    }
}
